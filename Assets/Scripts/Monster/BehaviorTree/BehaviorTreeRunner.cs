using System;
using UnityEngine;
using UnityEngine.XR;

public class BehaviorTreeRunner
{
    Node _rootNode;
    public BehaviorTreeRunner(Node rootNode)
    {
        _rootNode = rootNode;
    }

    public void Operate()
    {
        _rootNode.Evaluate();
    }
}