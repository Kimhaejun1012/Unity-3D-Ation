using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CheckWeakState : Node
{
    Blackboard _blackboard;
    Animator animator;

    public CheckWeakState(string name, Blackboard blackboard) : base(name)
    {
        _blackboard = blackboard;
        animator = _blackboard.GetValue<Animator>("Animator");
    }

    public override NodeState Evaluate()
    {
        if (animator.GetBool("WeakState"))
        {
            return NodeState.Failure;
        }
        return NodeState.Success;
    }
}
