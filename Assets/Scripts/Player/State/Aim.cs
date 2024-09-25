using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : State
{
    float smoothHorizontal = 0f;
    float smoothVertical = 0f;
    public Aim(PlayerController player, PlayerAnimationHandler animationHandler)
        : base(player, animationHandler)
    {
        applySpeed = player.aimSpeed;
    }
    public override void Enter()
    {
        GameManager.instance.CamZoomStart();
        animationHandler.SetBool("Aim", true);
    }

    public override void Exit()
    {
        animationHandler.SetBool("Aim", false);
        animationHandler.ResetTrigger("Attack");
        GameManager.instance.CamZoomFinish();
    }

    public override void Update()
    {
        dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        Vector3 moveH = Vector3.Scale(Camera.main.transform.right, new Vector3(1, 0, 1)) * dir.x;
        Vector3 moveV = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)) * dir.z;

        moveVec = (moveH + moveV).normalized;

        player.rb.velocity = moveVec * applySpeed;

        smoothHorizontal = Mathf.Lerp(smoothHorizontal, dir.x, 10f * Time.deltaTime);
        smoothVertical = Mathf.Lerp(smoothVertical, dir.z, 10f * Time.deltaTime);

        float cameraY = Camera.main.transform.eulerAngles.y;

        Quaternion targetRotationY = Quaternion.Euler(0f, cameraY, 0f);

        player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetRotationY, Time.deltaTime * 30f);

        animationHandler.SetFloat("BlendX", smoothHorizontal);
        animationHandler.SetFloat("BlendY", smoothVertical);
    }
}
