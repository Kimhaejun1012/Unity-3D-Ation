using UnityEngine;


public abstract class BaseWeapon : MonoBehaviour
{
    [SerializeField] protected WeaponStats weaponStats;
    protected PlayerController player;
    protected PlayerAnimationHandler animationHandler;
    public abstract void Init(PlayerController player, PlayerAnimationHandler animationHandler);
    public abstract void Exit();
    public abstract void Using();
}