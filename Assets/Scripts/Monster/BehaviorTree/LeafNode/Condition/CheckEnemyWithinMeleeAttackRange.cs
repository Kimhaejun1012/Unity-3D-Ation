using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnemyWithinMeleeAttackRange : Node
{
    Blackboard _blackboard;

    public CheckEnemyWithinMeleeAttackRange(string name, Blackboard blackboard) : base(name)
    {
        _blackboard = blackboard;
    }
    public override NodeState Evaluate()
    {
        var transform = _blackboard.GetValue<Transform>("Transform");
        var target = _blackboard.GetValue<Transform>("Target");

        if (target != null)
        {
            if (Vector3.SqrMagnitude(transform.position - target.position) < (3 * 3))
            {
                return NodeState.Success;
            }
        }
        return NodeState.Failure;
    }
}
