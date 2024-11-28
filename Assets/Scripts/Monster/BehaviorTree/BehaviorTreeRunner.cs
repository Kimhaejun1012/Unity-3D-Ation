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
        if (node.childs.Count != 0)
        {
            foreach (var child in node.childs)
            {
                ResetChildNodeState(child);
            }
        }
        node.nodeState = NodeState.Ready;
    }
}