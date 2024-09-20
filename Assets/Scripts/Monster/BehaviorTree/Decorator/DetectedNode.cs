using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class DetectedNode : Node
{
    Blackboard _blackboard;
    Node child;
    float detectRange = 20f;
    Transform transform;
    Transform target;

    public DetectedNode( string name, Node node, Blackboard blackboard) : base(name)
    {
        _blackboard = blackboard;
        transform = _blackboard.GetValue<Transform>("Transform");
        child = node;
        AddChild(child);
    }

    public override NodeState Evaluate()
    {
        var overlapColliders = Physics.OverlapSphere(transform.position, detectRange, LayerMask.GetMask("Player"));

        if (overlapColliders != null && overlapColliders.Length > 0)
        {
            target = overlapColliders[0].transform;
            _blackboard.SetValue("Target", target);
            return child.Evaluate();
        }
        else
        {
            return NodeState.Failure;
        }
    }
}
