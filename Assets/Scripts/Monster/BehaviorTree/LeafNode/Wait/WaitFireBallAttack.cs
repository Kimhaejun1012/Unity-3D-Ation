using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitFireBallAttack : Node
{
    Blackboard _blackboard;

    Animator animator;
    public WaitFireBallAttack(string name, Blackboard blackboard) : base(name)
    {
        _blackboard = blackboard;
        animator = _blackboard.GetValue<Animator>("Animator");
    }

    public override NodeState Evaluate()
    {
        throw new System.NotImplementedException();
    }
}
