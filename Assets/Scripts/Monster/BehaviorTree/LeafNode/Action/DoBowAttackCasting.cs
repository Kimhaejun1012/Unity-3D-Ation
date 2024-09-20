using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DoBowAttackCasting : Node
{
    Blackboard _blackboard;
    Animator animator;

    public DoBowAttackCasting(string name, Blackboard blackboard) : base(name)
    {
        _blackboard = blackboard;
        animator = blackboard.GetValue<Animator>("Animator");
    }

    public override NodeState Evaluate()
    {
        animator.SetTrigger("BowAttackCasting");
        if (animator.GetBool("Attacking"))
        {
            animator.ResetTrigger("BowAttackCasting");
        }
        return NodeState.Success;
    }
}
