using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : BaseWeapon
{
    public override void Init(PlayerController player, PlayerAnimationHandler animationHandler)
    {
        this.player = player;
        this.animationHandler = animationHandler;
        UIManager.instance.SetCrossHair();
    }
    public override void Exit(Transform weaponPack)
    {
        UIManager.instance.SetCrossHair();
    }

    public override void Using()
    {
        if(player.state != PlayerState.Jump && player.state != PlayerState.Run)
        {
            if (Input.GetMouseButtonDown(0))
            {
                player.ChangeState(PlayerState.Aim);
            }
            if (Input.GetMouseButton(0))
            {
            }
            if (Input.GetMouseButtonUp(0))
            {
                animationHandler.SetTrigger("Attack");
                Shot();
            }
        }
    }
    public void Shot()
    {
        var arrow = ObjectPoolManager.instance.GetPool("Arrow");
        arrow.GetComponent<Arrow>().Init(transform.position);
    }
}
