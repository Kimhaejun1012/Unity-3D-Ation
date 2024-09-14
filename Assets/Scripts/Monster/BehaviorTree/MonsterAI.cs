using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    BehaviorTreeRunner BTrunner;
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
    Node BTRunnerInit()
    {
        SelectorNode rootNode = new("Root Node");

        SequenceNode meleeAttack = new("MeleeAttack");
        meleeAttack.AddChild(new CheckMeleeAttacking("CheckMeleeAttacking"));
        meleeAttack.AddChild(new CheckEnemyWithinMeleeAttackRange("CheckEnemyWithinMeleeAttackRange"));
        meleeAttack.AddChild(new DoMeleeAttack("DoMeleeAttack"));

        SequenceNode moveDetectPlayer = new("MoveDetectPlayer");
        moveDetectPlayer.AddChild(new CheckDetectEnemy("CheckDetectEnemy"));
        moveDetectPlayer.AddChild(new MoveToDetectEnemy("MoveToDetectEnemy"));

        rootNode.AddChild(meleeAttack);
        rootNode.AddChild(moveDetectPlayer);
        rootNode.AddChild(new MoveToOriginPosition("MoveToOriginPosition", originPos));

        return rootNode;
    }
}
