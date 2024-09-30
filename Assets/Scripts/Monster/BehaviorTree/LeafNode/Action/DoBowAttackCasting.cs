using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DoBowAttackCasting : Node
{
    Blackboard _blackboard;
    Animator animator;

    public DoBowAttackCasting(string name, Blackboard blackboard) : base(name)
    {
        _blackboard = blackboard;
        animator = blackboard.GetValue<Animator>("Animator");
    }

    public override NodeState Evaluate()
    {
        if (animator.GetBool("Attacking"))
        {
            animator.ResetTrigger("BowAttackCasting");
            var agent = _blackboard.GetValue<NavMeshAgent>("NavMeshAgent");

            agent.isStopped = true;
        }
        else
        {
            var skillHandler = _blackboard.GetValue<SkillHandler>("SkillHandler");
            var castingTime = _blackboard.GetValue<float>("BlackHoleCastingTime");
            skillHandler.DoBlackHoleAttack(castingTime);
            animator.SetBool("Attacking", true);
            animator.SetTrigger("BowAttackCasting");
        }
        return NodeState.Success;
    }
}
