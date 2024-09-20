using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State
{
    public Idle(PlayerController player, PlayerAnimationHandler animationHandler)
        : base(player, animationHandler) { }

    public override void Enter()
    {

    }

    public override void Exit()
    {
    }
    public override void Update()
    {
        dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (dir != Vector3.zero)
        {
            if (Input.GetButton("Run"))
            {
                player.ChangeState(PlayerState.Run);
            }
            else
            {
                player.ChangeState(PlayerState.Walk);
            }
        }
        if (Input.GetButtonDown("Jump"))
        {
            player.PlayerJump();
        }
        if (Input.GetButtonDown("Crouch"))
        {
            player.ChangeState(PlayerState.Crouch);
        }
    }
}
