using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCoolDown : Node
{
    Blackboard _blackboard;
    float coolTime;
    public SkillCoolDown(string name, Blackboard blackboard) : base(name)
    {
        _blackboard = blackboard;
        coolTime = _blackboard.GetValue<float>("SkillCoolTime");
    }

    public override NodeState Evaluate()
    {
        var curCoolTime = _blackboard.GetValue<float>("CurSkillCool") + Time.deltaTime;
        _blackboard.SetValue("CurSkillCool", curCoolTime);

        UIManager.instance.skillCoolTime = $"SkillCoolTime = {coolTime - curCoolTime:F1}";

        if (curCoolTime > coolTime)
        {
            _blackboard.SetValue("SkillReady", true);
        }
        else
        {
            _blackboard.SetValue("SkillReady", false);
        }
        return NodeState.Failure;
    }
}
