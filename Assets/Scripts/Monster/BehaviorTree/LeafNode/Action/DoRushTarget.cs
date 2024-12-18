using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DoRushTarget : Node
{
    Blackboard _blackboard;
    Animator animator;

    float speed;
    float dashAttackRange = 5f;

    public DoRushTarget(string name, Blackboard blackboard) : base(name)
    {
        _blackboard = blackboard;
        animator = _blackboard.GetValue<Animator>("Animator");

        speed = _blackboard.GetValue<int>("DashSpeed");
    }

    public override NodeState Evaluate()
    {
        var transform = _blackboard.GetValue<Transform>("Transform");
        var target = _blackboard.GetValue<Transform>("Target");
        var agent = _blackboard.GetValue<NavMeshAgent>("NavMeshAgent");
        agent.speed = speed;

        if (animator.GetBool("Attacking"))
        {
            return NodeState.Failure;
        }

        if (Vector3.Distance(transform.position, target.position) >= dashAttackRange)
        {
            animator.SetBool("Dash", true);
            agent.isStopped = false;
            agent.SetDestination(target.position);
        }
        else if(animator.GetBool("Dash"))
        {
            animator.SetBool("Dash", false);
            agent.isStopped = true;
            return NodeState.Success;
        }
        return NodeState.Running;
    }
}
