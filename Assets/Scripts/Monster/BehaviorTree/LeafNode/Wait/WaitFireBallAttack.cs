using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitFireBallAttack : Node
{
    Blackboard _blackboard;
    public WaitFireBallAttack(string name, Blackboard blackboard) : base(name)
    {
        _blackboard = blackboard;
    }

    public override NodeState Evaluate()
    {
        throw new System.NotImplementedException();
    }
}
