using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeaterNode : Node
{
    int repeatCount;
    int counter;

    public RepeaterNode(string name, int count = 30) : base(name)
    {
        repeatCount = count;
        counter = 0;
    }

    public override NodeState Evaluate()
    {
        if (counter < repeatCount)
        {
            var state = childs[0].Evaluate();
            if (state == NodeState.Success || state == NodeState.Failure)
            {
                counter++;
            }
            return NodeState.Running;
        }
        return NodeState.Success;
    }
}
