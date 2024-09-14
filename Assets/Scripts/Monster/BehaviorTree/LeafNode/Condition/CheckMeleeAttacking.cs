using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMeleeAttacking : Node
{
    public CheckMeleeAttacking(string name) : base(name)
    {
    }

    public override NodeState Evaluate()
    {
        if (IsAnimationRunning("Attack"))
        {
            return NodeState.Running;
        }

        return NodeState.Success;
    }
    bool IsAnimationRunning(string stateName)
    {
        if (MonsterManager.instance.animator != null)
        {
            if (MonsterManager.instance.animator.GetCurrentAnimatorStateInfo(0).IsName(stateName))
            {
                var normalizedTime = MonsterManager.instance.animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

                return normalizedTime != 0 && normalizedTime < 1f;
            }
        }

        return false;
    }
}
