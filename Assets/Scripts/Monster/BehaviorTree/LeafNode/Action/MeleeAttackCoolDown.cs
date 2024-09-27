using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeAttackCoolDown : Node
{
    Blackboard _blackboard;
    float coolTime;

    public MeleeAttackCoolDown(string name, Blackboard blackboard) : base(name)
    {
        _blackboard = blackboard;
        coolTime = _blackboard.GetValue<float>("MeleeAttackCoolTime");
    }

    public override NodeState Evaluate()
    {
        var curCoolTime = _blackboard.GetValue<float>("CurMeleeCool") + Time.deltaTime;
        _blackboard.SetValue("CurMeleeCool", curCoolTime);
        UIManager.instance.meleeCoolTime = $"MeleeAttackCoolTime = {coolTime - curCoolTime:F1}";

        if (curCoolTime > coolTime)
        {
            _blackboard.SetValue("MeleeAttackReady", true);
        }
        else
        {
            _blackboard.SetValue("MeleeAttackReady", false);
        }
        return NodeState.Failure;
    }
}
