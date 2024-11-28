using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : BaseWeapon
{
    bool canParrying = false;
    bool canDodge = false;

    public GameObject parryingPrefab;
    public GameObject projectile;

    [SerializeField] Collider triggerCollider;
    [SerializeField] Collider nonTriggerCollider;
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
                //[TODO] 디스트로이가 아니라 리턴풀 해야됨
                projectile.GetComponent<IProjectile>().ReturnObject();
                var pos = projectile.transform.position;
                var temp = ObjectPoolManager.instance.GetPool("Parrying");
                var targetPos = projectile.GetComponent<IProjectile>().GetAttacker();
                var dir = targetPos.position - transform.position;
                temp.GetComponent<ParryingProjectile>().Init(dir, pos);
                TimeManager.instance.ParryingSlowMotion();
                projectile = null;
                canParrying = false;
            }
            else
            {
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
        triggerCollider.enabled = true;
        nonTriggerCollider.enabled = true;

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
        triggerCollider.enabled = false;
        nonTriggerCollider.enabled = false;
        switch (player.state)
        {
            case PlayerState.OnShield:
                player.SetStateIdle();
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyProjectile"))
        {
            animationHandler.SetTrigger("Block");
            projectile = other.gameObject;
            canParrying = true;
        }
        else if (other.CompareTag("MonsterWeapon"))
        {
            canDodge = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("EnemyProjectile"))
        {
            projectile = null;
            canParrying = false;
        }
        else if (other.CompareTag("MonsterWeapon"))
        {
            canDodge = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "MonsterWeapon")
        {
            animationHandler.SetTrigger("Block");
        }
            canParrying = false;
    }
}
