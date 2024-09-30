using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWeaponController : MonoBehaviour
{
    [SerializeField] MonsterWeapon weapon;
    public void ActivateCollider()
    {
        weapon.weaponCollider.enabled = true;
    }
    public void DeactivateCollider()
    {
        weapon.weaponCollider.enabled = false;
    }
}
