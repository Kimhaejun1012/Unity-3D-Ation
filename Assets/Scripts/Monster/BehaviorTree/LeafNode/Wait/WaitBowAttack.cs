using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitBowAttack : Node
{
    Blackboard _blackboard;

    Animator animator;
    public WaitBowAttack(string name, Blackboard blackboard) : base(name)
    {
        _blackboard = blackboard;
        animator = _blackboard.GetValue<Animator>("Animator");
    }

    public override NodeState Evaluate()
    {
        throw new System.NotImplementedException();
    }
}
