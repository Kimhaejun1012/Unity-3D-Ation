using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodge : State
{
    public Dodge(PlayerController player, PlayerAnimationHandler animationHandler) : base(player, animationHandler)
    {
    }
    public override void Enter()
    {
        animationHandler.SetTrigger("Dodge");
        Vector3 backRoll = -player.transform.forward + Vector3.up;
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
