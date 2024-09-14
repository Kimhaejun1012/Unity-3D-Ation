using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoMeleeAttack : Node
{
    public DoMeleeAttack(string name) : base(name)
    {
    }

    public override NodeState Evaluate()
    {
        MonsterManager.instance.animator.SetTrigger("Attack");
        return NodeState.Success;
    }
}
