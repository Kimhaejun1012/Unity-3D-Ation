using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CheckMeleeAttacking : Node
{
    Blackboard _blackboard;

    Animator animator;
    public CheckMeleeAttacking(string name, Blackboard blackboard) : base(name)
    {
        _blackboard = blackboard;
        animator = _blackboard.GetValue<Animator>("Animator");
    }

    public override NodeState Evaluate()
    {
        if (animator.GetBool("Attacking"))
        {
            return NodeState.Running;
        }
        return NodeState.Success;
    }
}
