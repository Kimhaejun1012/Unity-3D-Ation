using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class DoDashAttack : Node
{
    Blackboard _blackboard;

    Animator animator;
    public DoDashAttack(string name, Blackboard blackboard) : base(name)
    {
        _blackboard = blackboard;
        animator = _blackboard.GetValue<Animator>("Animator");
    }

    public override NodeState Evaluate()
    {
        animator.SetTrigger("DashAttack");
        _blackboard.SetValue("CurSkillCool", 0f);
        return NodeState.Success;
    }
}
