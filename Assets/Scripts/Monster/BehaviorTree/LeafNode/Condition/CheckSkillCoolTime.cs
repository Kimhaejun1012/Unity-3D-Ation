using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSkillCoolTime : Node
{
    Blackboard _blackboard;
    public CheckSkillCoolTime(string name, Blackboard blackboard) : base(name)
    {
        _blackboard = blackboard;
    }

    public override NodeState Evaluate()
    {
        var isReady = _blackboard.GetValue<bool>("SkillReady");

        if (isReady)
        {
            return NodeState.Success;
        }
        else
        {
            return NodeState.Failure;
        }
    }
}
