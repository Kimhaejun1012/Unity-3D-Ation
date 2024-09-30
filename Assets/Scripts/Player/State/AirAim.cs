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
        animationHandler.ResetTrigger("Cancel");
        animationHandler.SetTrigger("AirAim");
        TimeManager.instance.ApplySlowMotion();
    }

    public override void Exit()
    {
        animationHandler.ResetTrigger("Attack");
        animationHandler.SetTrigger("Cancel");
        GameManager.instance.CamZoomFinish();
        TimeManager.instance.SetTimeScaleOne();
    }

    public override void Update()
    {
        float cameraY = Camera.main.transform.eulerAngles.y;

        Quaternion targetRotationY = Quaternion.Euler(0f, cameraY, 0f);

        player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetRotationY, Time.deltaTime * 30f / Time.timeScale);

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
