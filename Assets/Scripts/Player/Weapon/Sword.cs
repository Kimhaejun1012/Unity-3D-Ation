using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : BaseWeapon
{
    public override void Init(PlayerController player, PlayerAnimationHandler animationHandler)
    {
        this.player = player;
        this.animationHandler = animationHandler;
        this.animationHandler.SetBool("Sword", true);
    }
    public override void Exit()
    {
        animationHandler.SetBool("Sword", false);
    }

    public override void Using()
    {
        if (Input.GetMouseButtonDown(0))
        {
            switch (player.state)
            {
                case PlayerState.Idle:
                case PlayerState.Walk:
                    animationHandler.SetTrigger("Attack");
                    player.ChangeState(PlayerState.Attack);
                    break;
                case PlayerState.Run:
                    animationHandler.SetTrigger("Attack");
                    break;
                case PlayerState.Air:
                    animationHandler.SetTrigger("Attack");
                    player.ChangeState(PlayerState.JumpAttack);
                    break;
                case PlayerState.Attack:
                    animationHandler.SetTrigger("Attack");
                    break;
                case PlayerState.Dodge:
                    //if (!Physics.Raycast(player.transform.position + Vector3.up * 0.2f, Vector3.down, 0.2f, player.groundLayer))
                    //{
                        player.ChangeState(PlayerState.DodgeAttack);
                    animationHandler.SetTrigger("Attack");
                    //}
                    break;
                case PlayerState.DodgeAttack:
                    animationHandler.SetTrigger("Attack");
                    break;
                default:
                    break;
            }
        }
    }
}
