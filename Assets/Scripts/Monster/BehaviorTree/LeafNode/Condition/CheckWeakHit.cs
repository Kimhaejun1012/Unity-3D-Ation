using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWeakHit : Node
{
    Blackboard _blackboard;
    Animator animator;
    public CheckWeakHit(string name, Blackboard blackboard) : base(name)
    {
        _blackboard = blackboard;
        animator = _blackboard.GetValue<Animator>("Animator");
    }

    public override NodeState Evaluate()
    {
        var isWeakHit = _blackboard.GetValue<bool>("WeakHit");
        if(isWeakHit)
        {
            animator.SetTrigger("WeakHit");
            return NodeState.Success;
        }
        else
        {
            return NodeState.Failure;
        }
    }
}
