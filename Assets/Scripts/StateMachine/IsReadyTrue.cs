using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsReadyTrue : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("IsReady", true);
    }
}
