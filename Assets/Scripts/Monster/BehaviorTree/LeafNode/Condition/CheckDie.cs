using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CheckDie : Node
{
    Blackboard _blackboard;

    ActorStats stats;
    public CheckDie(string name, Blackboard blackboard) : base(name)
    {
        _blackboard = blackboard;
        stats = _blackboard.GetValue<ActorStats>("ActorStats");
    }

    public override NodeState Evaluate()
    {
        if (stats.HP <= 0)
        {
            return NodeState.Success;
        }
        else
        {
            return NodeState.Failure;
        }
    }
}
