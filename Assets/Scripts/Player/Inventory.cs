using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    WeaponController controller;

    public List<BaseWeapon> weapons;

    private void Awake()
    {
        controller = GetComponent<WeaponController>();
    }
    void Start()
    {
        controller.ChangeWeapon(weapons[1], weapons[2]);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            controller.ChangeWeapon(weapons[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            controller.ChangeWeapon(weapons[1], weapons[2]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            controller.ChangeWeapon();
        }
    }
}
