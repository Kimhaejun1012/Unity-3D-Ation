using UnityEngine;

public class DodgeAttack : State
{
    private Transform targetMonster;
    private float approachDistance = 2f;
    bool isArrive = false;
    bool isLanding = false;
    private float speed = 5f;

    public DodgeAttack(PlayerController player, PlayerAnimationHandler animationHandler)
        : base(player, animationHandler)
    {
    }

    public override void Enter()
    {
        player.isKnockBack = true;
        isLanding = false;
        isArrive = false;
        targetMonster = FindClosestMonster();
    }

    public override void Exit()
    {
        TimeManager.instance.SetTimeScaleOne();
        player.isKnockBack = false;
    }

    public override void Update()
    {
        if (Physics.Raycast(player.transform.position + Vector3.up * 0.2f, Vector3.down, 0.2f, player.groundLayer) && !isLanding)
        {
            isLanding = true;
            TimeManager.instance.ApplySlowMotion();
            animationHandler.SetTrigger("Dash");
        }
        else if (targetMonster != null && isLanding)
        {
            float distance = Vector3.Distance(player.transform.position, targetMonster.position);

            if (distance > approachDistance)
            {
                player.transform.position = Vector3.Lerp(player.transform.position, targetMonster.position, speed * Time.unscaledDeltaTime);
            }
            else if (!isArrive)
            {
                animationHandler.SetTrigger("DashOut");
                isArrive = true;
            }
        }
    }

    Transform FindClosestMonster()
    {
        float closestDistanceSqr = Mathf.Infinity;
        Transform closestEnemy = null;
        Vector3 currentPosition = player.transform.position;
        float detectionRadius = 30.0f;

        Collider[] colliders = Physics.OverlapSphere(currentPosition, detectionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("Monster"))
            {
                Vector3 directionToTarget = collider.transform.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    closestEnemy = collider.transform;
                }
            }
        }

        return closestEnemy;
    }

}
