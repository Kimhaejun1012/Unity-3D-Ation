using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MoveToDetectEnemy : Node
{
    Blackboard _blackboard;

    Animator animator;
    int speed;
    public MoveToDetectEnemy(string name, Blackboard blackboard) : base(name)
    {
        _blackboard = blackboard;
        animator = _blackboard.GetValue<Animator>("Animator");

        speed = _blackboard.GetValue<int>("WalkSpeed");
    }

    public override NodeState Evaluate()
    {
        var transform = _blackboard.GetValue<Transform>("Transform");
        var target = _blackboard.GetValue<Transform>("Target");
        if (Vector3.SqrMagnitude(transform.position - target.position) < (2 * 2))
        {
            animator.SetBool("Walk", false);
            return NodeState.Success;
        }
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * speed);
        transform.LookAt(target);
        animator.SetBool("Walk", true);
        return NodeState.Running;
    }
}
