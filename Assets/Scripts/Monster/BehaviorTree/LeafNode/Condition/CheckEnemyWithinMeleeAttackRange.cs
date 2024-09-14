using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnemyWithinMeleeAttackRange : Node
{
    public CheckEnemyWithinMeleeAttackRange(string name) : base(name)
    {
    }

    public override NodeState Evaluate()
    {
        if (MonsterManager.instance.transform_P != null)
        {
            if (Vector3.SqrMagnitude(MonsterManager.instance.transform_P.position - MonsterManager.instance.transform_M.position) < (5 * 5))
            {
                return NodeState.Success;
            }
        }
        return NodeState.Failure;
    }
}
