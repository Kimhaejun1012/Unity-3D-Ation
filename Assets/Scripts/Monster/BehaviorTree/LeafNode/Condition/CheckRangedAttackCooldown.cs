using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CheckRangedAttackCooldown : Node
{
    Blackboard _blackboard;

    Animator animator;

    private float cooldownTime = 5f;
    private float lastTime = -Mathf.Infinity;
    public CheckRangedAttackCooldown(string name, Blackboard blackboard) : base(name)
    {
        _blackboard = blackboard;
        animator = _blackboard.GetValue<Animator>("Animator");
    }

    public override NodeState Evaluate()
    {
        if (Time.time > lastTime + cooldownTime)
        {
            lastTime = Time.time;
            return NodeState.Success;
        }
        return NodeState.Failure;
    }
}