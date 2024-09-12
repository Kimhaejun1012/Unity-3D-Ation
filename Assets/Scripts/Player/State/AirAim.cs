using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirAim : State
{
    public AirAim(PlayerController player, PlayerAnimationHandler animationHandler)
        : base(player, animationHandler)
    {
        applySpeed = player.aimSpeed;
    }
    public override void Enter()
    {
        animationHandler.SetTrigger("AirAim");
    }

    public override void Exit()
    {
        animationHandler.ResetTrigger("Attack");
    }

    public override void Update()
    {
        float cameraY = Camera.main.transform.eulerAngles.y;

        Quaternion targetRotationY = Quaternion.Euler(0f, cameraY, 0f);

        player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetRotationY, Time.deltaTime * 30f);
    }
}
