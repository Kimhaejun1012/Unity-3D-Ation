using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMeleeAttacking : Node
{
    Blackboard _blackboard;

    Animator animator;
    Transform transform;
    Transform target;
    public CheckMeleeAttacking(string name, Blackboard blackboard) : base(name)
    {
        _blackboard = blackboard;
        animator = _blackboard.GetValue<Animator>("Animator");
    }

    public override NodeState Evaluate()
    {
        if (animator.GetBool("Attacking"))
        {
            transform = _blackboard.GetValue<Transform>("Transform");
            target = _blackboard.GetValue<Transform>("Target");
            transform.LookAt(target);
            return NodeState.Running;
        }
        return NodeState.Success;
    }
}
