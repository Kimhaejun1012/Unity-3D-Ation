using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    BehaviorTreeRunner BTrunner;
    public Blackboard blackboard = new();
    Vector3 originPos;

    [SerializeField] Collider weakPoint;

    [SerializeField] float rangeAttackCoolTime = 10f;
    [SerializeField] float blackHoleCastingTime = 3f;
    [SerializeField] float fireBallCastingTime = 3f;
    [SerializeField] float meleeAttackCoolTime = 5f;

    [SerializeField] bool onGUI;

    [SerializeField] int walkSpeed;
    [SerializeField] int dashSpeed;

    private void Start()
    {
        originPos = transform.position;
        BTrunner = new BehaviorTreeRunner(BTRunnerInit());
    }

    void Update()
    {
        BTrunner?.Operate();
    }

    #region DrawNodeState
    private void OnGUI()
    {
        if (onGUI)
        {
            DrawNode(BTrunner.RootNode);
        }
    }

    void DrawNode(Node node, int depth = 0)
    {
        GUIStyle style = new GUIStyle();
        switch (node.nodeState)
        {
            case NodeState.Running:
                style.normal.textColor = Color.green;
                break;
            case NodeState.Success:
                style.normal.textColor = Color.yellow;
                break;
            case NodeState.Failure:
                style.normal.textColor = Color.red;
                break;
            case NodeState.Ready:
                style.normal.textColor = Color.black;
                break;
        }

        int indentSize = 20;
        GUILayout.BeginHorizontal();
        GUILayout.Space(depth * indentSize);
        GUILayout.Label(node.name, style);
        GUILayout.EndHorizontal();

        foreach (Node child in node._childs)
        {
            DrawNode(child, depth + 1);
        }
    }
    #endregion
    Node BTRunnerInit()
    {
        Blackboard blackboard = BlackBoardInit();

        SelectorNode rootNode = new("Root Node");

        SequenceNode dieSequence = new("DieSequence");
        dieSequence.AddChild(new CheckDie("CheckDie", blackboard));
        dieSequence.AddChild(new DoDieAction("DoDieAction", blackboard));

        SequenceNode hitWeakSequence = new("HitWeakSequence");
        hitWeakSequence.AddChild(new CheckWeakHit("CheckWeakHit", blackboard));
        hitWeakSequence.AddChild(new DelayNode("WeakHitDelay", 4f, new DoWeakHitAction("DoWeakHitAction", blackboard)));

        Node skillCoolDown = new SkillCoolDown("skillCoolDown" , blackboard);
        Node meleeCoolDown = new MeleeAttackCoolDown("MeleeCoolDown", blackboard);

        rootNode.AddChild(dieSequence);

        rootNode.AddChild(skillCoolDown);
        rootNode.AddChild(meleeCoolDown);

        rootNode.AddChild(hitWeakSequence);

        SetDetectedNode(rootNode);

        rootNode.AddChild(new MoveToOriginPosition("MoveToOriginPosition", originPos, blackboard));

        return rootNode;
    }

    #region 블랙보드Init
    Blackboard BlackBoardInit()
    {
        var animator = GetComponent<Animator>();
        var transform = GetComponent<Transform>();
        var rigidbody = GetComponent<Rigidbody>();
        var actorStats = GetComponent<ActorStats>();
        var skillHandler = GetComponent<SkillHandler>();
        var navMeshAgent = GetComponent<NavMeshAgent>();

        blackboard.SetValue("Animator", animator);
        blackboard.SetValue("Transform", transform);
        blackboard.SetValue("Rigidbody", rigidbody);
        blackboard.SetValue("Rigidbody", rigidbody);
        blackboard.SetValue("ActorStats", actorStats);
        blackboard.SetValue("SkillHandler", skillHandler);
        blackboard.SetValue("NavMeshAgent", navMeshAgent);

        blackboard.SetValue("WalkSpeed", walkSpeed);
        blackboard.SetValue("DashSpeed", dashSpeed);
        blackboard.SetValue("BlackHoleCastingTime", blackHoleCastingTime);
        blackboard.SetValue("FireBallCastingTime", fireBallCastingTime);

        blackboard.SetValue("SkillCoolTime", rangeAttackCoolTime);
        blackboard.SetValue("MeleeAttackCoolTime", meleeAttackCoolTime);
        blackboard.SetValue("CurSkillCool", 0f);
        blackboard.SetValue("CurMeleeCool", 0f);

        blackboard.SetValue("WeakHit", false);
        blackboard.SetValue("SkillReady", false);
        blackboard.SetValue("MeleeAttackReady", false);

        return blackboard;
    }
    #endregion

    void SetDetectedNode(Node rootNode)
    {
        SelectorNode dectedSelector = new("DectedSelector");
        dectedSelector.AddChild(SetSkillNode());

        DetectedNode detectedNode = new("CheckDetected", dectedSelector, blackboard);
        dectedSelector.AddChild(SetMeleeAttackNode());

        SequenceNode moveSequence = new("MoveSequence");
        moveSequence.AddChild(new MoveToDetectEnemy("MoveToDetectEnemy", blackboard));

        dectedSelector.AddChild(moveSequence);
        rootNode.AddChild(detectedNode);
    }

    Node SetSkillNode()
    {
        SequenceNode skillSequence = new("Skill");

        RandomSelectorNode randomSelector = new("RangeAttackRandomSelector");


        skillSequence.AddChild(new CheckMeleeAttacking("CheckAttacking", blackboard));
        skillSequence.AddChild(new CheckSkillCoolTime("CheckSkillCoolTime",blackboard));
        skillSequence.AddChild(randomSelector);

        SequenceNode fireBallSequence = new("FireBallSequence");
        fireBallSequence.AddChild(new DoFireBallAttackCasting("WaitFireBallAttack", blackboard));
        fireBallSequence.AddChild(new DelayNode("FireBallDelay", fireBallCastingTime, new DoFireBallAttack("FireBall", blackboard)));

        SequenceNode bowAttackSequence = new("BowAttackSequence");
        bowAttackSequence.AddChild(new DoBowAttackCasting("WaitBowAttack", blackboard));
        bowAttackSequence.AddChild(new DelayNode("BowDelay", blackHoleCastingTime, new DoBowAttack("BowAttack", blackboard)));

        SequenceNode dashAttackSequence = new("DashAttackSequence");
        dashAttackSequence.AddChild(new DoRushTarget("DoRushTarget", blackboard));
        dashAttackSequence.AddChild(new DoDashAttack("DashAttack", blackboard));

        randomSelector.AddChild(fireBallSequence);
        randomSelector.AddChild(bowAttackSequence);
        randomSelector.AddChild(dashAttackSequence);

        return skillSequence;
    }
    Node SetMeleeAttackNode()
    {
        SequenceNode meleeAttackSequence = new("MeleeAttackSequence");

        meleeAttackSequence.AddChild(new CheckMeleeAttacking("CheckMeleeAttacking", blackboard));

        meleeAttackSequence.AddChild(new CheckMeleeAttackCoolTime("CheckMeleeAttackCoolTime", blackboard));
        meleeAttackSequence.AddChild(new CheckEnemyWithinMeleeAttackRange("CheckEnemyWithinMeleeAttackRange", blackboard));
        meleeAttackSequence.AddChild(new DoMeleeAttack("DoMeleeAttack", blackboard));

        return meleeAttackSequence;
    }

    public void DestroyMonsterAI()
    {
        Destroy(GetComponent<CapsuleCollider>());
        Destroy(this);
    }
    public void WeakHit(Collider collider)
    {
        if (collider == weakPoint && !blackboard.GetValue<bool>("WeakHit"))
        {
            blackboard.SetValue("WeakHit", true);
        }
    }
}
