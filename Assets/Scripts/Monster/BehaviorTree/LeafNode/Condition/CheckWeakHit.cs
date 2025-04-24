using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CheckWeakHit : Node
{
    Blackboard _blackboard;
    Animator animator;

    NavMeshAgent agent;

    public CheckWeakHit(string name, Blackboard blackboard) : base(name)
    {
        _blackboard = blackboard;
        animator = _blackboard.GetValue<Animator>("Animator");
        agent = _blackboard.GetValue<NavMeshAgent>("NavMeshAgent");
    }

    public override NodeState Evaluate()
    {
        var isWeakHit = _blackboard.GetValue<bool>("WeakHit");
        if(isWeakHit)
        {
            animator.SetTrigger("WeakHit");

            agent.isStopped = true;
            return NodeState.Success;
        }
        else
        {
            return NodeState.Failure;
        }
    }
}
