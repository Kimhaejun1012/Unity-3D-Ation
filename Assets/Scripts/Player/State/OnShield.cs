using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnShield : State
{
    float smoothVertical;
    public OnShield(PlayerController player, PlayerAnimationHandler animationHandler) : base(player, animationHandler)
    {
        applySpeed = player.aimSpeed;
    }

    public override void Enter()
    {
        animationHandler.SetBool("OnShield", true);
    }

    public override void Exit()
    {
        animationHandler.SetBool("OnShield", false);
    }

    public override void Update()
    {
        if(!animationHandler.IsAnimationRunning("Parrying"))
        {
            dir.z = Input.GetAxis("Vertical");

            smoothVertical = Mathf.Lerp(smoothVertical, dir.z, 10f * Time.deltaTime);
            float cameraY = Camera.main.transform.eulerAngles.y;
            Quaternion targetRotationY = Quaternion.Euler(0f, cameraY, 0f);
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetRotationY, Time.deltaTime * 30f);
            player.rb.velocity = player.transform.forward * (dir.z * applySpeed);

            animationHandler.SetFloat("BlendY", smoothVertical);
        }
    }
}
