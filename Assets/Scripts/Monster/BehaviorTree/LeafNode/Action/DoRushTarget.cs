using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.XR;

public class DoRushTarget : Node
{
    Blackboard _blackboard;
    Animator animator;

    float speed = 10f;
    float dashAttackRange = 5f;

    public DoRushTarget(string name, Blackboard blackboard) : base(name)
    {
        _blackboard = blackboard;
        animator = _blackboard.GetValue<Animator>("Animator");
    }

    public override NodeState Evaluate()
    {
        var transform = _blackboard.GetValue<Transform>("Transform");
        var target = _blackboard.GetValue<Transform>("Target");
        if (Vector3.Distance(transform.position, target.position) <= dashAttackRange)
        {
            animator.SetTrigger("Dash");
            transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * speed);
            transform.LookAt(target.position);
        }
        else
        {
            animator.ResetTrigger("Dash");
            return NodeState.Success;
        }
        return NodeState.Running;
    }
}
