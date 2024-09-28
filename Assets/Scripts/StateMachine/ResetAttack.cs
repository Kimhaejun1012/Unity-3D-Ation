using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAttack : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator != null)
        {
            animator.ResetTrigger("Attack");
        }
    }
}
