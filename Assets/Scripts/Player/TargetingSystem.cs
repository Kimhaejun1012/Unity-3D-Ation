using System.Collections.Generic;
using UnityEngine;

public class TargetingSystem : MonoBehaviour
{
    public float detectionRadius = 20f;
    public float fieldOfViewAngle = 150f;
    public float viewDistance = 10f;

    public GameObject targetObj;
    public LayerMask enemyLayer;

    Transform player;
    Transform currentTarget;
    PlayerController playerController;

    private void Awake()
    {
        player = GetComponent<Transform>();
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            currentTarget = FindClosestTargetInView();
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            if (currentTarget != null)
            {
                playerController.SetStateIdle();
                currentTarget = null;
            }
        }
        if (currentTarget != null)
        {
            player.LookAt(currentTarget);
            targetObj.SetActive(true);
            Vector3 headPosition = currentTarget.position + new Vector3(0, 4, 0);
            targetObj.transform.position = headPosition;
            Debug.Log("현재 타겟 = " + currentTarget.name);
        }
        else
        {
            targetObj.SetActive(false);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 leftBoundary = Quaternion.Euler(0, -fieldOfViewAngle / 2, 0) * transform.forward;
        Vector3 rightBoundary = Quaternion.Euler(0, fieldOfViewAngle / 2, 0) * transform.forward;

        Gizmos.DrawRay(transform.position, leftBoundary * viewDistance);
        Gizmos.DrawRay(transform.position, rightBoundary * viewDistance);
    }

    Transform FindClosestTargetInView()
    {
        Collider[] hits = Physics.OverlapSphere(player.position, detectionRadius, enemyLayer);
        Transform closestTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider hit in hits)
        {
            Debug.Log("범위 안에 들어온 오브젝트 = " + hit.name);
            Vector3 directionToTarget = hit.transform.position - player.position;
            float angle = Vector3.Angle(directionToTarget, player.forward);

            if (angle < fieldOfViewAngle / 2)
            {
                float distance = directionToTarget.magnitude;
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = hit.transform;
                }
            }
        }

        if(closestTarget != null)
        {
            playerController.ChangeState(PlayerState.Aim);
        }

        return closestTarget;
    }
}
