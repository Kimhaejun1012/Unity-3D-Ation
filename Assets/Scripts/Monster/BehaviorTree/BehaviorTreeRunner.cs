using System;
using UnityEngine;
using UnityEngine.XR;

public class BehaviorTreeRunner
{
    Node _rootNode;
    public Node RootNode { get { return _rootNode; } }
    public BehaviorTreeRunner(Node rootNode)
    {
        _rootNode = rootNode;
    }

    public void Operate()
    {
        ResetChildNodeState(_rootNode);
        _rootNode.Evaluate();
    }
    public void ResetChildNodeState(Node node)
    {
        if (node._childs.Count != 0)
        {
            foreach (var child in node._childs)
            {
                ResetChildNodeState(child);
            }
        }
        node.nodeState = NodeState.Ready;
    }
}