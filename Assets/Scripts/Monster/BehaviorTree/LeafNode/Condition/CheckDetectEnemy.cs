using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDetectEnemy : Node
{
    public CheckDetectEnemy(string name) : base(name)
    {
    }

    public override NodeState Evaluate()
    {
        var overlapColliders = Physics.OverlapSphere(MonsterManager.instance.transform_M.position, 10f, LayerMask.GetMask("Player"));

        if (overlapColliders != null && overlapColliders.Length > 0)
        {
            MonsterManager.instance.transform_P = overlapColliders[0].transform;

            return NodeState.Success;
        }


        return NodeState.Failure;
    }
}
