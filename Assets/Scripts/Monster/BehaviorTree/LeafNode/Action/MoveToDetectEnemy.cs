using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToDetectEnemy : Node
{
    public MoveToDetectEnemy(string name) : base(name)
    {
    }

    public override NodeState Evaluate()
    {
        if (Vector3.SqrMagnitude(MonsterManager.instance.transform_P.position - MonsterManager.instance.transform_M.position) < (5 * 5))
        {
            return NodeState.Success;
        }

        MonsterManager.instance.transform_M.position = Vector3.MoveTowards(MonsterManager.instance.transform_M.position, MonsterManager.instance.transform_P.position, Time.deltaTime * 10);
        return NodeState.Running;
    }
}
