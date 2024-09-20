using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

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
        return NodeState.Success;

        //return NodeState.Success;
    }
}
