using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodge : State
{
    float upPower = 6f;
    float backPower = 3f;
    public Dodge(PlayerController player, PlayerAnimationHandler animationHandler) : base(player, animationHandler)
    {
    }
    public override void Enter()
    {
        animationHandler.SetTrigger("Dodge");
        Vector3 backRoll = -player.transform.forward * backPower + Vector3.up * upPower;
        backRoll.Normalize();
        player.rb.AddForce(backRoll * 10f, ForceMode.Impulse);
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
    }
}
