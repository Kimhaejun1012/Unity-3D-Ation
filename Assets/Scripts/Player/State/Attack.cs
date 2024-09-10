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
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
        return;
    }
}
