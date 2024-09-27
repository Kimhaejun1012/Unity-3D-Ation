using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMeleeAttackCoolTime : Node
{
    Blackboard _blackboard;
    public CheckMeleeAttackCoolTime(string name, Blackboard blackboard) : base(name)
    {
        _blackboard = blackboard;
    }

    public override NodeState Evaluate()
    {
        var isReady = _blackboard.GetValue<bool>("MeleeAttackReady");

        if(isReady)
        {
            return NodeState.Success;
        }
        else
        {
            return NodeState.Failure;
        }
    }
}
