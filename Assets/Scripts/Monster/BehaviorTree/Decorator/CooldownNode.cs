using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

public class CooldownNode : Node
{
    Node _node;
    float cooldownTime;
    float curTime;

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
        float remainingTime = cooldownTime - curTime;
        coolDown.Clear();
        coolDown.AppendFormat("{0}: {1:F1}", name, remainingTime);
        curTime += Time.deltaTime;
        if (curTime > cooldownTime)
        {
            _node.nodeState = _node.Evaluate();
            if (_node.nodeState == NodeState.Success)
            {
                curTime = 0;
            }
            return _node.nodeState;
        }
        _node.nodeState = NodeState.Ready;
        return NodeState.Failure;
    }
}
