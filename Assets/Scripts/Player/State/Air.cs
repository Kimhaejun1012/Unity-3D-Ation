using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Air : State
{
    public Air(PlayerController player, PlayerAnimationHandler animationHandler)
        : base(player, animationHandler)
    {
    }
    public override void Enter()
    {
        animationHandler.ResetTrigger("Landing");
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
        if (player.rb.velocity.y <= 0)
        {
            if (Physics.Raycast(player.transform.position + Vector3.up * 0.2f, Vector3.down, 0.2f, player.groundLayer))
            {
                animationHandler.SetTrigger("Landing");
                player.SetStateIdle();
            }
        }
    }
}
