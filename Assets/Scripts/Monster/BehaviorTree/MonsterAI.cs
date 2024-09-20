using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    BehaviorTreeRunner BTrunner;
    Blackboard blackboard = new();
    Vector3 originPos;
    private void Start()
    {
        originPos = transform.position;
        BTrunner = new BehaviorTreeRunner(BTRunnerInit());
    }

    void Update()
    {
        BTrunner?.Operate();
    }
    private void OnGUI()
    {
        DrawNode(BTrunner.RootNode);
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

    Node BTRunnerInit()
    {
        Blackboard blackboard = BlackBoardInit();

        SelectorNode rootNode = new("Root Node");

        SequenceNode dieSequence = new("DieSequence");
        dieSequence.AddChild(new CheckDie("CheckDie", blackboard));
        dieSequence.AddChild(new DoDieAction("DoDieAction", blackboard));

        rootNode.AddChild(dieSequence);

        SetDetectedNode(rootNode);

        rootNode.AddChild(new MoveToOriginPosition("MoveToOriginPosition", originPos, blackboard));

        return rootNode;
    }

    Blackboard BlackBoardInit()
    {

        var animator = GetComponent<Animator>();
        var transform = GetComponent<Transform>();
        var rigidbody = GetComponent<Rigidbody>();
        var actorStats = GetComponent<ActorStats>();

        blackboard.SetValue("Animator", animator);
        blackboard.SetValue("Transform", transform);
        blackboard.SetValue("Rigidbody", rigidbody);
        blackboard.SetValue("Rigidbody", rigidbody);
        blackboard.SetValue("ActorStats", actorStats);

        return blackboard;
    }

    void SetDetectedNode(Node rootNode)
    {
        SelectorNode dectedSelector = new("DectedSelector");

        RandomSelectorNode randomSelector = new("RangeAttackRandomSelector");
        CooldownNode rangeAttackCoolDown = new("RangeAttackCoolDown", 105f, randomSelector);

        SequenceNode fireBallSequence = new("FireBallSequence");
        fireBallSequence.AddChild(new DoFireBallAttackCasting("WaitFireBallAttack", blackboard));
        fireBallSequence.AddChild(new CooldownNode("FireBallCoolDown", 3f, new DoFireBallAttack("FireBall", blackboard)));

        SequenceNode bowAttackSequence = new("BowAttackSequence");
        bowAttackSequence.AddChild(new DoFireBallAttackCasting("WaitBowAttack", blackboard));
        bowAttackSequence.AddChild(new CooldownNode("BowCoolDown", 3f, new DoBowAttack("BowAttack", blackboard)));

        SequenceNode dashAttackSequence = new("DashAttackSequence");
        dashAttackSequence.AddChild(new DoRushTarget("DoRushTarget", blackboard));
        dashAttackSequence.AddChild(new DoDashAttack("DashAttack", blackboard));

        randomSelector.AddChild(fireBallSequence);
        randomSelector.AddChild(bowAttackSequence);
        randomSelector.AddChild(dashAttackSequence);

        dectedSelector.AddChild(rangeAttackCoolDown);


        DetectedNode detectedNode = new("CheckDetected", dectedSelector, blackboard);

        rootNode.AddChild(detectedNode);

        SequenceNode meleeAttackSequence = new("MeleeAttackSequence");
        meleeAttackSequence.AddChild(new CheckMeleeAttacking("CheckMeleeAttacking", blackboard));
        meleeAttackSequence.AddChild(new CheckEnemyWithinMeleeAttackRange("CheckEnemyWithinMeleeAttackRange", blackboard));
        CooldownNode cooldownNode =
            new CooldownNode("MeleeAttackCoolDown", 5f, new DoMeleeAttack("DoMeleeAttack", blackboard));
        meleeAttackSequence.AddChild(cooldownNode);

        dectedSelector.AddChild(meleeAttackSequence);

        SequenceNode moveSequence = new("MoveSequence");
        //moveSequence.AddChild(new CheckDetectEnemy("CheckDetectEnemy", blackboard));
        moveSequence.AddChild(new MoveToDetectEnemy("MoveToDetectEnemy", blackboard));

        dectedSelector.AddChild(moveSequence);
    }
}
