using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : State
{
    public Attack(PlayerController player, PlayerAnimationHandler animationHandler)
    : base(player, animationHandler)
    {
    }
    public override void Enter()
    {
        animationHandler.SetBool("Attacking", true);
    }

    public override void Exit()
    {
        animationHandler.SetBool("Attacking", false);
    }

    public override void Update()
    {
        return;
    }
}
