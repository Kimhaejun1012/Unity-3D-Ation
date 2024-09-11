using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : BaseWeapon
{
    private bool canParrying = false;
    
    public Vector3 projectileVelocity;
    public GameObject parryingPrefab;
    public GameObject projectile;
    public override void Init(PlayerController player, PlayerAnimationHandler animationHandler)
    {
        this.player = player;
        this.animationHandler = animationHandler;
    }
    public override void Exit(Transform weaponPack)
    {
    }
    public override void Using()
    {
        if (Input.GetMouseButtonDown(1))
        {
            player.ChangeState(PlayerState.OnShield);
        }
        if (Input.GetMouseButtonUp(1))
        {
            player.SetStateIdle();
        }
        if (Input.GetKeyDown(KeyCode.E) && player.state == PlayerState.OnShield)
        {
            if (canParrying)
            {
                var temp = Instantiate(parryingPrefab, transform.position, Quaternion.identity);
                temp.GetComponent<Rigidbody>().AddForce(-projectileVelocity * 10f, ForceMode.Impulse);
                Destroy(projectile);
                Debug.Log(" 패링 성공 ");
            }
            else
            {
                Debug.Log(" 패링 실패 ");
            }
            animationHandler.SetTrigger("Parrying");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            projectile = other.gameObject;
            projectileVelocity = projectile.transform.GetComponent<Rigidbody>().velocity;
            canParrying = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            projectile = null;
            canParrying = false;
        }
    }
}
