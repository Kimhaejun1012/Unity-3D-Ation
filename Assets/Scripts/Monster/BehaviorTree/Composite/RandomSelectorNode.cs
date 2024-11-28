using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.XR;

public sealed class RandomSelectorNode : Node
{
    Random random;
    int? currentChildIndex;

    int count = 0;

    public RandomSelectorNode(string name) : base(name)
    {
        random = new Random();
        currentChildIndex = null;
    }

    public override NodeState Evaluate()
    {
        if (childs == null || childs.Count == 0)
            return NodeState.Failure;


        #region 스킬 1,2,3순서대로 진행 후 랜덤
        //if (count < childs.Count && !currentChildIndex.HasValue)
        //{
        //    currentChildIndex = count;
        //    count++;
        //}
        //else if (!currentChildIndex.HasValue)
        //{
        //    currentChildIndex = random.Next(childs.Count);
        //}
        #endregion
        #region 스킬랜덤진행
        //if (!currentChildIndex.HasValue)
        //{
        //    currentChildIndex = random.Next(childs.Count);
        //}
        #endregion
        #region 원하는 스킬 입력
        if (!currentChildIndex.HasValue)
        {
            currentChildIndex = 0;
        }
        #endregion

        NodeState result = childs[currentChildIndex.Value].Evaluate();

        switch (result)
        {
            case NodeState.Running:
                childs[currentChildIndex.Value].nodeState = NodeState.Running;
                return NodeState.Running;
            case NodeState.Success:
                childs[currentChildIndex.Value].nodeState = NodeState.Success;
                currentChildIndex = null;
                break;
            case NodeState.Failure:
                childs[currentChildIndex.Value].nodeState = NodeState.Failure;
                currentChildIndex = null;
                break;
        }
        return result;
    }
}
