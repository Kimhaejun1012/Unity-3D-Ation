using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToOriginPosition : Node
{
    Vector3 _originPos;
    
    public MoveToOriginPosition(string name, Vector3 originPos) : base(name)
    {
        _originPos = originPos;
    }

    public override NodeState Evaluate()
    {
        if (Vector3.SqrMagnitude(_originPos - MonsterManager.instance.transform_M.position) < float.Epsilon * float.Epsilon)
        {
            return NodeState.Success;
        }
        else
        {
            MonsterManager.instance.transform_M.position = Vector3.MoveTowards(MonsterManager.instance.transform_M.position, _originPos, Time.deltaTime * 10);
            return NodeState.Running;
        }
    }
}
