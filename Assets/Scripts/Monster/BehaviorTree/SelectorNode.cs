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
                    return NodeState.Running;
                case NodeState.Success:
                    return NodeState.Success;
            }
        }
        return NodeState.Failure;
    }
}