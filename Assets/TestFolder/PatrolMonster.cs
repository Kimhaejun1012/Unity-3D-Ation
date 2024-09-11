using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolMonster : MonoBehaviour
{
    public Transform rallyPoint1;
    public Transform rallyPoint2;
    Vector3 nextCheckPos;
    int rallyPointIndex = 1;
    public float speed = 5f;

    void Start()
    {
        nextCheckPos = rallyPoint1.position;
    }

    void Update()
    {
        Vector3 direction = (nextCheckPos - transform.position).normalized;

        transform.Translate(direction * speed * Time.deltaTime, Space.World);
        transform.LookAt(nextCheckPos);

        if (Vector3.Distance(transform.position, nextCheckPos) < 0.1f)
        {
            if (rallyPointIndex == 1)
            {
                nextCheckPos = rallyPoint2.position;
                rallyPointIndex = 2;
            }
            else
            {
                nextCheckPos = rallyPoint1.position;
                rallyPointIndex = 1;
            }
        }
    }
}
