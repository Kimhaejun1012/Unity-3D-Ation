using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodge : State
{
    float upPower = 2f;
    float backPower = 1f;
    public Dodge(PlayerController player, PlayerAnimationHandler animationHandler) : base(player, animationHandler)
    {
    }
    public override void Enter()
    {
        player.isKnockBack = true;
        animationHandler.SetTrigger("Dodge");
        Vector3 backRoll = -player.transform.forward * backPower + Vector3.up * upPower;
        backRoll.Normalize();
        player.rb.AddForce(backRoll * 6f, ForceMode.Impulse);
    }

    public override void Exit()
    {
        player.isKnockBack = false;
    }

    public override void Update()
    {
        if (player.rb.velocity.y < 0)
        {
            if (Physics.Raycast(player.transform.position + Vector3.up * 0.2f, Vector3.down, 0.2f, player.groundLayer))
            {
                animationHandler.SetTrigger("Landing");
                player.SetStateIdle();
            }
        }
    }
}
