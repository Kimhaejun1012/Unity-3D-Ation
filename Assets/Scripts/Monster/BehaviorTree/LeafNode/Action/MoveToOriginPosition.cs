using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class MoveToOriginPosition : Node
{
    Blackboard _blackboard;

    Animator animator;
    private int speed;
    Vector3 _originPos;
    
    public MoveToOriginPosition(string name, Vector3 originPos, Blackboard blackboard) : base(name)
    {
        _blackboard = blackboard;
        _originPos = originPos;
        animator = _blackboard.GetValue<Animator>("Animator");
        speed = _blackboard.GetValue<int>("WalkSpeed");

    }

    public override NodeState Evaluate()
    {
        var agent = _blackboard.GetValue<NavMeshAgent>("NavMeshAgent");
        agent.speed = speed;
        var transform = _blackboard.GetValue<Transform>("Transform");

        if (Vector3.SqrMagnitude(_originPos - transform.position) < float.Epsilon * float.Epsilon)
        {
            animator.SetBool("Walk", false);
            return NodeState.Success;
        }
        else
        {
            agent.SetDestination(_originPos);
            animator.SetBool("Walk", true);
            return NodeState.Running;
        }
    }
}
