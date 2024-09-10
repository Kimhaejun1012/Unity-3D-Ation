using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : State
{
    public Jump(PlayerController player, PlayerAnimationHandler animationHandler) : base(player, animationHandler) { }

    public override void Enter()
    {
        animationHandler.SetTrigger("Jump");
        Vector3 jumpForce = Vector3.up * player.jumpPower;
        player.rb.AddForce(jumpForce, ForceMode.VelocityChange);
    }

    public override void Exit()
    {
        animationHandler.ResetTrigger("Landing");
    }

    public override void Update()
    {
        if (player.rb.velocity.y < 0)
        {
            if (Physics.Raycast(player.transform.position + Vector3.up * 0.2f, Vector3.down, 0.2f, player.groundLayer))
            {
                animationHandler.SetTrigger("Landing");
            }
        }
    }
}
