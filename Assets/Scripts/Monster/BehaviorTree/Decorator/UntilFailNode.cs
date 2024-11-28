using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UntilFailNode : Node
{
    public UntilFailNode(string name) : base(name) { }

    public override NodeState Evaluate()
    {
        NodeState childState = childs[0].Evaluate();
        if (childState == NodeState.Failure)
        {
            return NodeState.Failure;
        }
        return NodeState.Running;
    }
}
