using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum WeaponState
{
    Bow,
    Sword,
}

public class WeaponController : MonoBehaviour
{
    public BaseWeapon myWeapon;
    public BaseWeapon myShield = null;
    public Transform rightWeaponPoint;
    public Transform leftWeaponPoint;

    private PlayerController player;
    private PlayerAnimationHandler animationHandler;
    public Transform weaponPackPoint;

    public WeaponState state;

    private void Awake()
    {
        player = GetComponent<PlayerController>();
        animationHandler = GetComponent<PlayerAnimationHandler>();
    }
    private void Start()
    {
    }

    void Update()
    {
        myWeapon?.Using();
        myShield?.Using();
    }

    public void ChangeWeapon(BaseWeapon weapon)
    {
        DestoryPreWeapon();

        state = WeaponState.Bow;

        myWeapon = Instantiate(weapon);
        myWeapon.transform.SetParent(leftWeaponPoint, false);
        myWeapon.Init(player, animationHandler);
    }
    public void ChangeWeapon(BaseWeapon weapon, BaseWeapon shield)
    {
        DestoryPreWeapon();
        state = WeaponState.Sword;
        myWeapon = Instantiate(weapon);
        myWeapon.transform.SetParent(rightWeaponPoint, false);
        myWeapon.Init(player, animationHandler);

        myShield = Instantiate(shield);
        myShield.transform.SetParent(leftWeaponPoint, false);
        myShield.Init(player, animationHandler);
    }
    public void ChangeWeapon()
    {
        DestoryPreWeapon();
    }
    void DestoryPreWeapon()
    {
        if (myWeapon != null)
        {
            myWeapon.Exit();
            Destroy(myWeapon.gameObject);
            myWeapon = null;
        }
        if (myShield != null)
        {
            myShield.Exit();
            Destroy(myShield.gameObject);
            myShield = null;
        }
    }
}
