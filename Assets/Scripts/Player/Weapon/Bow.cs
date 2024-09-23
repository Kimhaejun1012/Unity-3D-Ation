using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : BaseWeapon
{
    public override void Init(PlayerController player, PlayerAnimationHandler animationHandler)
    {
        this.player = player;
        this.animationHandler = animationHandler;
        this.animationHandler.SetBool("Bow", true);
        UIManager.instance.SetCrossHair();
    }
    public override void Exit()
    {
        animationHandler.SetBool("Bow", false);
        UIManager.instance.SetCrossHair();
    }

    public override void Using()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleMouseDown();
        }
        if (Input.GetMouseButtonUp(0))
        {
            HandleMouseUp();
        }
    }
    private void HandleMouseDown()
    {
        switch (player.state)
        {
            case PlayerState.Idle:
            case PlayerState.Walk:
                player.ChangeState(PlayerState.Aim);
                break;
            case PlayerState.Dodge:
                TryChangeToAirAim();
                break;
            case PlayerState.Air:
                TryChangeToAirAim();
                break;
        }
    }
    private void HandleMouseUp()
    {
        switch (player.state)
        {
            case PlayerState.Idle:
            case PlayerState.Walk:
            case PlayerState.AirAim:
            case PlayerState.Aim:
                PerformAttack();
                break;
        }
    }

    void TryChangeToAirAim()
    {
        if (!Physics.Raycast(player.transform.position + Vector3.up * 0.2f, Vector3.down, 0.5f, player.groundLayer))
        {
            player.ChangeState(PlayerState.AirAim);
        }
    }

    void PerformAttack()
    {
        animationHandler.SetTrigger("Attack");
    }
    public void Shot()
    {
        var arrow = ObjectPoolManager.instance.GetPool("Arrow");
        arrow.GetComponent<Arrow>().Init(transform.position, weaponStats.damage);
    }
}
