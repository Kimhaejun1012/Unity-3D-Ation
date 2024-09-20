using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MoveToOriginPosition : Node
{
    Blackboard _blackboard;

    Animator animator;
    Vector3 _originPos;
    
    public MoveToOriginPosition(string name, Vector3 originPos, Blackboard blackboard) : base(name)
    {
        _blackboard = blackboard;
        _originPos = originPos;
        animator = _blackboard.GetValue<Animator>("Animator");
    }

    public override NodeState Evaluate()
    {
        var transform = _blackboard.GetValue<Transform>("Transform");

        if (Vector3.SqrMagnitude(_originPos - MonsterManager.instance.transform_M.position) < float.Epsilon * float.Epsilon)
        {
            animator.SetBool("Walk", false);
            return NodeState.Success;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _originPos, Time.deltaTime * 10);
            transform.LookAt(_originPos);
            animator.SetBool("Walk", true);
            return NodeState.Running;
        }
    }
}
