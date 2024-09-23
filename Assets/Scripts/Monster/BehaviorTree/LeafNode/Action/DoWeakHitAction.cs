using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoWeakHitAction : Node
{
    Blackboard _blackboard;
    Animator animator;
    public DoWeakHitAction(string name, Blackboard blackboard) : base(name)
    {
        _blackboard = blackboard;
        animator = _blackboard.GetValue<Animator>("Animator");
    }

    public override NodeState Evaluate()
    {
        if (animator != null)
        {
            _blackboard.SetValue("WeakHit", false);
            animator.SetTrigger("Recover");
            return NodeState.Success;
        }
        return NodeState.Failure;
    }
}
