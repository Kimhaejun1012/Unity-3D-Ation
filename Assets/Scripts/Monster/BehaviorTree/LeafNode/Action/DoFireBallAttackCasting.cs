using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

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
        if (animator.GetBool("SkillCasting"))
        {
            //animator.ResetTrigger("FireBallCasting");
            var agent = _blackboard.GetValue<NavMeshAgent>("NavMeshAgent");
            agent.isStopped = true;

            var transform = _blackboard.GetValue<Transform>("Transform");
            var target = _blackboard.GetValue<Transform>("Target");

            Vector3 targetDirection = target.position - transform.position;

            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 20f * Time.deltaTime);
        }
        else
        {
            var skillHandler = _blackboard.GetValue<SkillHandler>("SkillHandler");
            var fireballCastingTime = _blackboard.GetValue<float>("FireBallCastingTime");
            skillHandler.DoFireballSkill(fireballCastingTime);
            animator.SetBool("SkillCasting", true);
            animator.SetTrigger("FireBallCasting");
        }
        return NodeState.Success;
    }
}
