using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.AI;

public class DoWeakHitAction : Node
{
    Blackboard _blackboard;
    Animator animator;
    NavMeshAgent agent;
    public DoWeakHitAction(string name, Blackboard blackboard) : base(name)
    {
        _blackboard = blackboard;
        animator = _blackboard.GetValue<Animator>("Animator");
        agent = _blackboard.GetValue<NavMeshAgent>("NavMeshAgent");

    }

    public override NodeState Evaluate()
    {
        if (animator != null)
        {
            _blackboard.SetValue("WeakHit", false);
            animator.SetTrigger("Recover");
            agent.isStopped = false;

            return NodeState.Success;
        }
        return NodeState.Failure;
    }
}
