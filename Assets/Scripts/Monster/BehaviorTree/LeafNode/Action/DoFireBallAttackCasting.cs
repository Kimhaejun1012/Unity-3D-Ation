using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoFireBallAttackCasting : Node
{
    Blackboard _blackboard;

    Animator animator;

    public DoFireBallAttackCasting(string name, Blackboard blackboard) : base(name)
    {
        _blackboard = blackboard;
        animator = blackboard.GetValue<Animator>("Animator");
    }

    public override NodeState Evaluate()
    {
        animator.SetTrigger("FireBallCasting");
        if (animator.GetBool("Attacking"))
        {
            animator.ResetTrigger("FireBallCasting");
        }
        return NodeState.Success;
    }
}
