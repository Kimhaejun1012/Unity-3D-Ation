using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnShield : State
{
    float smoothHorizontal = 0f;
    float smoothVertical = 0f;
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
            dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            Vector3 moveH = Vector3.Scale(Camera.main.transform.right, new Vector3(1, 0, 1)) * dir.x;
            Vector3 moveV = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)) * dir.z;

            moveVec = (moveH + moveV).normalized;

            float cameraY = Camera.main.transform.eulerAngles.y;
            Quaternion targetRotationY = Quaternion.Euler(0f, cameraY, 0f);

            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetRotationY, Time.deltaTime * 30f);

            player.rb.velocity = moveVec * applySpeed;

            smoothHorizontal = Mathf.Lerp(smoothHorizontal, dir.x, 10f * Time.deltaTime);
            smoothVertical = Mathf.Lerp(smoothVertical, dir.z, 10f * Time.deltaTime);

            animationHandler.SetFloat("BlendX", smoothHorizontal);
            animationHandler.SetFloat("BlendY", smoothVertical);
        }
    }
}
