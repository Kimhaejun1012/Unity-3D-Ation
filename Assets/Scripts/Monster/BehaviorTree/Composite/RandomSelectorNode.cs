using System;
using System.Collections.Generic;
using UnityEngine.XR;

public sealed class RandomSelectorNode : Node
{
    private Random _random;
    private int? _currentChildIndex;

    public RandomSelectorNode(string name) : base(name)
    {
        _random = new Random();
        _currentChildIndex = null;
    }

    public override NodeState Evaluate()
    {
        if (_childs == null || _childs.Count == 0)
            return NodeState.Failure;

        if (!_currentChildIndex.HasValue)
        {
            //_currentChildIndex = _random.Next(_childs.Count);
            _currentChildIndex = 1;
        }

        NodeState result = _childs[_currentChildIndex.Value].Evaluate();

        switch (result)
        {
            case NodeState.Running:
                _childs[_currentChildIndex.Value].nodeState = NodeState.Running;
                return NodeState.Running;
            case NodeState.Success:
                _childs[_currentChildIndex.Value].nodeState = NodeState.Success;
                _currentChildIndex = null;
                break;
            case NodeState.Failure:
                _childs[_currentChildIndex.Value].nodeState = NodeState.Failure;
                _currentChildIndex = null;
                break;
        }
        return result;
    }
}
