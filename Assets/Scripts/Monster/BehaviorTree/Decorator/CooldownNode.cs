using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CooldownNode : Node
{
    Node _node;
    private float cooldownTime;
    private float lastTime;

    StringBuilder coolDown = new StringBuilder();
    public CooldownNode(string name, float cooldown, Node node) : base(name)
    {
        cooldownTime = cooldown;
        _node = node;
        AddChild(node);

        UIManager.instance.coolDowns.Add(coolDown);
        coolDown.AppendFormat("{0}: {1:F1}", name, cooldownTime);
    }

    public override NodeState Evaluate()
    {
        float remainingTime = Mathf.Max(0, (lastTime + cooldownTime) - Time.time);
        coolDown.Clear();
        coolDown.AppendFormat("{0}: {1:F1}", name, remainingTime);
        if (Time.time > lastTime + cooldownTime)
        {
            _node.nodeState = _node.Evaluate();
            if (_node.nodeState == NodeState.Success)
            {
                lastTime = Time.time;
            }
            return _node.nodeState;
        }
        _node.nodeState = NodeState.Ready;
        return NodeState.Failure;
    }
}
