using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : BaseWeapon
{
    public override void Init(PlayerController player, PlayerAnimationHandler animationHandler)
    {
        this.player = player;
        this.animationHandler = animationHandler;
    }
    public override void Exit(Transform weaponPack)
    {

    }
    public override void Using()
    {
        if (Input.GetMouseButtonDown(1))
        {
            player.ChangeState(PlayerState.OnShield);
        }
        if (Input.GetMouseButtonUp(1))
        {
            player.SetStateIdle();
        }
        if (Input.GetKeyDown(KeyCode.E) && player.state == PlayerState.OnShield)
        {
            animationHandler.SetTrigger("Parrying");
        }
    }
}
