using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    protected PlayerController player;
    protected PlayerAnimationHandler animationHandler;
    public abstract void Init(PlayerController player, PlayerAnimationHandler animationHandler);
    public abstract void Exit(Transform weaponPack);
    public abstract void Using();
}