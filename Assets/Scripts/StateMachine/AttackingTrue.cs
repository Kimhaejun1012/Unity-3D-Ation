using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingTrue : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Attacking", true);
    }
}
