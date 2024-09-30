using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkiilCastingTrue : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("SkillCasting", true);
    }
}
