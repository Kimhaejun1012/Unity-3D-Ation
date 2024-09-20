using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CheckEnemyWithinMeleeAttackRange : Node
{
    Blackboard _blackboard;

    Animator animator;
    public CheckEnemyWithinMeleeAttackRange(string name, Blackboard blackboard) : base(name)
    {
        _blackboard = blackboard;
        animator = _blackboard.GetValue<Animator>("Animator");
    }
    public override NodeState Evaluate()
    {
        if (MonsterManager.instance.transform_P != null)
        {
            if (Vector3.SqrMagnitude(MonsterManager.instance.transform_P.position - MonsterManager.instance.transform_M.position) < (2 * 2))
            {
                return NodeState.Success;
            }
        }
        return NodeState.Failure;
    }
}
