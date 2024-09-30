using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeState
{
    Ready,
    Running,
    Success,
    Failure,
}
public abstract class Node
{
    public List<Node> _childs = new();
    public NodeState nodeState;
    public string name;
    protected int childCount;
    public Node(string name)
    {
        this.name = name;
    }

    public abstract NodeState Evaluate();

    public void AddChild(Node node) => _childs.Add(node);
}
