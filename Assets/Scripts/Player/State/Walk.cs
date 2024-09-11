using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : State
{
    float smoothHorizontal = 0f;
    float smoothVertical = 0f;
    public Walk(PlayerController player, PlayerAnimationHandler animationHandler)
        : base(player, animationHandler)
    {
        applySpeed = player.walkSpeed;
    }
    public override void Enter()
    {
        if (Input.GetButton("Run"))
        {
            player.ChangeState(PlayerState.Run);
        }
        animationHandler.SetBool("Walk", true);
    }

    public override void Exit()
    {
        animationHandler.SetBool("Walk", false);
    }
    public override void Update()
    {
        dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        Vector3 moveH = Vector3.Scale(Camera.main.transform.right, new Vector3(1, 0, 1)) * dir.x;
        Vector3 moveV = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)) * dir.z;

        moveVec = (moveH + moveV).normalized;
        float timeScale = 1f / Time.timeScale;
        player.rb.velocity = moveVec * applySpeed * timeScale;

        smoothHorizontal = Mathf.Lerp(smoothHorizontal, dir.x, 10f * Time.deltaTime);
        smoothVertical = Mathf.Lerp(smoothVertical, dir.z, 10f * Time.deltaTime);
        animationHandler.SetFloat("BlendX", smoothHorizontal);
        animationHandler.SetFloat("BlendY", smoothVertical);

        if (dir != Vector3.zero)
        {
            Vector3 lookForward = Camera.main.transform.forward * dir.z;
            Vector3 lookRight = Camera.main.transform.right * dir.x;
            Vector3 rotate = Vector3.Scale((lookForward + lookRight).normalized, new Vector3(1, 0, 1));

            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.LookRotation(rotate), Time.deltaTime * player.rotSpeed);
        }

        if (dir == Vector3.zero)
        {
            player.ChangeState(PlayerState.Idle);
        }
        if (Input.GetButtonDown("Run"))
        {
            player.ChangeState(PlayerState.Run);
        }
        if (Input.GetButtonDown("Jump"))
        {
            player.ChangeState(PlayerState.Jump);
        }
        if (Input.GetButtonDown("Crouch"))
        {
            player.ChangeState(PlayerState.Crouch);
        }
    }
}
