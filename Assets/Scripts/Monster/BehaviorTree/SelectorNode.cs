using System.Collections.Generic;
using UnityEngine.XR;

public sealed class SelectorNode : Node
{
    public SelectorNode(string name) : base(name) { }
    public override NodeState Evaluate()
    {
        if (_childs == null)
            return NodeState.Failure;

        foreach (var child in _childs)
        {
            switch (child.Evaluate())
            {
                case NodeState.Running:
                    child.nodeState = NodeState.Running;
                    return NodeState.Running;
                case NodeState.Success:
                    child.nodeState = NodeState.Success;
                    return NodeState.Success;
                case NodeState.Failure:
                    child.nodeState = NodeState.Failure;
                    continue;
            }
        }
        return NodeState.Failure;
    }
}