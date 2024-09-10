using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : BaseWeapon
{
    public override void Exit(Transform weaponPack)
    {
    }

    public override void Init(PlayerController player, PlayerAnimationHandler animationHandler)
    {
        this.player = player;
        this.animationHandler = animationHandler;
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
                case PlayerState.Jump:
                    animationHandler.SetTrigger("Attack");
                    player.ChangeState(PlayerState.JumpAttack);
                    break;
                case PlayerState.Attack:
                    animationHandler.SetTrigger("Attack");
                    break;
                default:
                    break;
            }
        }
    }
}
