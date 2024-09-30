using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : State
{
    float riseTime = 1f;
    float curDownTime = 0f;
    public KnockBack(PlayerController player, PlayerAnimationHandler animationHandler) : base(player, animationHandler)
    {
    }

    public override void Enter()
    {
        player.isKnockBack = true;
        Vector3 forceDir = new Vector3(player.hitDir.x, 0.5f, player.hitDir.z);
        player.rb.AddForce(forceDir * 2f, ForceMode.Impulse);
        animationHandler.SetTrigger("Hit");
    }

    public override void Exit()
    {
        player.isKnockBack = false;
        curDownTime = 0;
    }

    public override void Update()
    {
        curDownTime += Time.deltaTime;
        if(curDownTime > riseTime)
        {
            animationHandler.SetTrigger("Rise");
            curDownTime = 0;
        }
    }
}
