using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public BaseWeapon myWeapon;
    public BaseWeapon myShield = null;
    public Transform rightWeaponPoint;
    public Transform leftWeaponPoint;

    private PlayerController player;
    private PlayerAnimationHandler animationHandler;
    public Transform weaponPackPoint;

    private void Awake()
    {
        player = GetComponent<PlayerController>();
        animationHandler = GetComponent<PlayerAnimationHandler>();
    }
    private void Start()
    {
        myWeapon = Instantiate(myWeapon);
        myWeapon.transform.SetParent(leftWeaponPoint, false);
        myWeapon.Init(player, animationHandler);

        //myShield = Instantiate(myShield);
        //myShield.transform.SetParent(leftWeaponPoint, false);
        //myShield.Init(player, animationHandler);
    }

    void Update()
    {
        myWeapon?.Using();
        myShield?.Using();
    }
}
