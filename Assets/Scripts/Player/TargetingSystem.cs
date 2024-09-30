using System.Collections.Generic;
using UnityEngine;

public class TargetingSystem : MonoBehaviour
{
    public delegate void TargetChanged(Transform newTarget);
    public static event TargetChanged OnTargeting;

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
            if (currentTarget == null)
            {
                currentTarget = FindClosestTargetInView();
            }
            else
            {
                currentTarget = null;
                playerController.TargetingBool();
            }
            OnTargeting?.Invoke(currentTarget);
        }
        //}
        //else if (Input.GetKeyUp(KeyCode.F))
        //{
        //    if (currentTarget != null)
        //    {
        //        playerController.TargetingBool();
        //        currentTarget = null;
        //        OnTargeting?.Invoke(currentTarget);
        //    }
        //}
        if (currentTarget != null)
        {
            //player.LookAt(currentTarget);
            float cameraY = Camera.main.transform.eulerAngles.y;

            Quaternion targetRotationY = Quaternion.Euler(0f, cameraY, 0f);
            player.transform.rotation = targetRotationY;
            targetObj.SetActive(true);
            Vector3 headPosition = currentTarget.position + new Vector3(0, 5, 0);
            targetObj.transform.position = headPosition;
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
            playerController.TargetingBool();
        }

        return closestTarget;
    }
}
