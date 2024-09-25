using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : State
{
    float riseTime = 3f;
    float curDownTime = 0f;
    public KnockBack(PlayerController player, PlayerAnimationHandler animationHandler) : base(player, animationHandler)
    {
    }

    public override void Enter()
    {
        Vector3 forceDir = new Vector3(player.hitDir.x, 0.3f, player.hitDir.z);
        player.rb.AddForce(forceDir * 30f, ForceMode.Impulse);
        animationHandler.SetTrigger("Hit");
    }

    public override void Exit()
    {
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
