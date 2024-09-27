using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DoFireBallAttackCasting : Node
{
    Blackboard _blackboard;

    Animator animator;

    public DoFireBallAttackCasting(string name, Blackboard blackboard) : base(name)
    {
        _blackboard = blackboard;
        animator = blackboard.GetValue<Animator>("Animator");
    }

    public override NodeState Evaluate()
    {
        if (animator.GetBool("Attacking"))
        {
            //animator.ResetTrigger("FireBallCasting");
            var agent = _blackboard.GetValue<NavMeshAgent>("NavMeshAgent");
            agent.isStopped = true;
        }
        else
        {
            var skillHandler = _blackboard.GetValue<SkillHandler>("SkillHandler");
            var fireballCastingTime = _blackboard.GetValue<float>("FireBallCastingTime");
            skillHandler.DoFireballSkill(fireballCastingTime);
            animator.SetBool("Attacking", true);
            animator.SetTrigger("FireBallCasting");
        }
        return NodeState.Success;

    }

}
