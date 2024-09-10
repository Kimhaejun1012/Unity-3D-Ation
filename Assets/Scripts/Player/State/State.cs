using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected PlayerController player;
    protected PlayerAnimationHandler animationHandler;

    protected Vector3 dir;
    protected float applySpeed;
    protected Vector3 moveVec;

    public State(PlayerController player, PlayerAnimationHandler animationHandler)
    {
        this.player = player;
        this.animationHandler = animationHandler;
    }
    public abstract void Enter();
    public abstract void Exit();
    public abstract void Update();
}
