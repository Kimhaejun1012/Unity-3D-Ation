using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class DoMeleeAttack : Node
{
    Blackboard _blackboard;

    Animator animator;
    public DoMeleeAttack(string name, Blackboard blackboard) : base(name)
    {
        _blackboard = blackboard;
        animator = _blackboard.GetValue<Animator>("Animator");
    }

    public override NodeState Evaluate()
    {
        animator.SetTrigger("MeleeAttack");
        _blackboard.SetValue("CurMeleeCool", 0f);
        var agent = _blackboard.GetValue<NavMeshAgent>("NavMeshAgent");
        agent.isStopped = true;
        return NodeState.Success;

        //return NodeState.Success;
    }
}
