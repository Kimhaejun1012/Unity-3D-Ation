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

            Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
            transform.LookAt(targetPosition);

            return NodeState.Running;
        }
        return NodeState.Success;
    }
}
