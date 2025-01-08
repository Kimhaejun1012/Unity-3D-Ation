using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CheckSkillCasting : Node
{
    Blackboard _blackboard;

    Animator animator;
    public CheckSkillCasting(string name, Blackboard blackboard) : base(name)
    {
        _blackboard = blackboard;
        animator = _blackboard.GetValue<Animator>("Animator");
    }
    public override NodeState Evaluate()
    {
        if (animator.GetBool("SkillCasting"))
        {
            return NodeState.Running;
        }
        return NodeState.Failure;
    }
}
