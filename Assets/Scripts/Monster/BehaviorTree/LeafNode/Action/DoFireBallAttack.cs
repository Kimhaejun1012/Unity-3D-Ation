using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class DoFireBallAttack : Node
{
    Blackboard _blackboard;

    Animator animator;
    public DoFireBallAttack(string name, Blackboard blackboard) : base(name)
    {
        _blackboard = blackboard;
        animator = _blackboard.GetValue<Animator>("Animator");
    }

    public override NodeState Evaluate()
    {
        var transform = _blackboard.GetValue<Transform>("Transform");
        var target = _blackboard.GetValue<Transform>("Target");

        transform.LookAt(target);
        animator.SetTrigger("FireBall");
        _blackboard.SetValue("CurSkillCool", 0f);

        return NodeState.Success;
    }
}
