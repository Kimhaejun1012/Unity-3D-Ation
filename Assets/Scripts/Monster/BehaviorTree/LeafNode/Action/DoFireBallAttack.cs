using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.XR;

public class DoFireBallAttack : Node
{
    Blackboard _blackboard;

    Animator animator;
    public DoFireBallAttack(string name, Blackboard blackboard) : base(name)
    {
        _blackboard = blackboard;
        animator = _blackboard.GetValue<Animator>("Animator");
    }

    public override NodeState Evaluate()
    {
        animator.SetTrigger("FireBall");
        return NodeState.Success;
    }
}
