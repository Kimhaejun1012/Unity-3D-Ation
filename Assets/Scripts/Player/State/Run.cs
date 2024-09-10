using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : State
{
    public Run(PlayerController player, PlayerAnimationHandler animationHandler) : base(player, animationHandler)
    {
        applySpeed = player.runSpeed;
    }
    public override void Enter()
    {
        animationHandler.SetBool("Run", true);
    }

    public override void Exit()
    {
        animationHandler.SetBool("Run", false);
    }

    public override void Update()
    {
        dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        Vector3 moveH = Vector3.Scale(Camera.main.transform.right, new Vector3(1, 0, 1)) * dir.x;
        Vector3 moveV = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)) * dir.z;

        moveVec = (moveH + moveV).normalized;

        player.rb.velocity = moveVec * applySpeed;

        if (dir != Vector3.zero)
        {
            Vector3 lookForward = Camera.main.transform.forward * dir.z;
            Vector3 lookRight = Camera.main.transform.right * dir.x;
            Vector3 rotate = Vector3.Scale((lookForward + lookRight).normalized, new Vector3(1, 0, 1));

            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.LookRotation(rotate), Time.deltaTime * player.rotSpeed);
        }

        if (!Input.GetButton("Run"))
        {
            player.ChangeState(PlayerState.Walk);
        }
        if (Input.GetButtonDown("Jump"))
        {
            animationHandler.SetTrigger("Jump");
            player.ChangeState(PlayerState.Jump);
        }
    }
}
