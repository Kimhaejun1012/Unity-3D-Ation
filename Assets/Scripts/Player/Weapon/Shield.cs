using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : BaseWeapon
{
    private bool canParrying = false;
    private bool canDodge = false;

    public Vector3 projectileVelocity;
    public GameObject parryingPrefab;
    public GameObject projectile;
    public override void Init(PlayerController player, PlayerAnimationHandler animationHandler)
    {
        this.player = player;
        this.animationHandler = animationHandler;
    }
    public override void Exit()
    {
    }
    public override void Using()
    {
        if (Input.GetMouseButtonDown(1))
        {
            HandleMouseDown();
        }
        if (Input.GetMouseButtonUp(1))
        {
            HandleMouseUp();
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
        if (Input.GetKeyDown(KeyCode.Space) && player.state == PlayerState.OnShield)
        {
            if(canDodge)
            {
                player.ChangeState(PlayerState.Dodge);
            }
            else
            {
            }
        }
    }
    private void HandleMouseDown()
    {
        switch (player.state)
        {
            case PlayerState.Idle:
            case PlayerState.Walk:
                player.ChangeState(PlayerState.OnShield);
                break;
            case PlayerState.Dodge:
                break;
            case PlayerState.Air:
                break;
        }
    }
    private void HandleMouseUp()
    {
        switch (player.state)
        {
            case PlayerState.OnShield:
                player.SetStateIdle();
                break;
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
        else if(other.CompareTag("MonsterWeapon"))
        {
            canDodge = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            projectile = null;
            canParrying = false;
        }
        else if (other.CompareTag("MonsterWeapon"))
        {
            canDodge = false;
        }
    }
}
