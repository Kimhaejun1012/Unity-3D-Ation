using System.Collections.Generic;
using UnityEngine.XR;

public sealed class SequenceNode : Node
{
    public SequenceNode(string name) : base(name) { }
    public override NodeState Evaluate()
    {
        if (_childs == null || _childs.Count == 0)
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
                    continue;
                case NodeState.Failure:
                    child.nodeState = NodeState.Failure;
                    return NodeState.Failure;
            }
        }
        return NodeState.Success;
    }
}