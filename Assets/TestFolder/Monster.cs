using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float meleeAttackCoolDown = 4f;
    float curMeleeAttackCoolDown;
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        curMeleeAttackCoolDown += Time.deltaTime;

        if(curMeleeAttackCoolDown > meleeAttackCoolDown)
        {
            animator.SetTrigger("Attack");
            curMeleeAttackCoolDown = 0f;
        }
    }
}
