using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverterNode : Node
{
    public InverterNode(string name) : base(name) { }

    public override NodeState Evaluate()
    {
        NodeState state = childs[0].Evaluate();
        if (state == NodeState.Success)
        {
            return NodeState.Failure;
        }
        else if (state == NodeState.Failure)
        {
            return NodeState.Success;
        }
        return state;
    }
}
