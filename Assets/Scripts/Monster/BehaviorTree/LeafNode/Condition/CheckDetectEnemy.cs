using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDetectEnemy : Node
{
    Blackboard _blackboard;

    Animator animator;
    public CheckDetectEnemy(string name, Blackboard blackboard) : base(name)
    {
        _blackboard = blackboard;
        animator = _blackboard.GetValue<Animator>("Animator");
    }

    public override NodeState Evaluate()
    {
        //var overlapColliders = Physics.OverlapSphere(MonsterManager.instance.transform_M.position, 10f, LayerMask.GetMask("Player"));

        //if (overlapColliders != null && overlapColliders.Length > 0)
        //{
        //    MonsterManager.instance.transform_P = overlapColliders[0].transform;

        //    return NodeState.Success;
        //}


        return NodeState.Failure;
    }
}
