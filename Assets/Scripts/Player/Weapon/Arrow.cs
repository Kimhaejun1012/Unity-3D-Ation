using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Arrow : MonoBehaviour
{
    private Rigidbody rb;
    public LayerMask monsterLayer;
    public int damage = 5;

    readonly float power = 30;
    readonly bool isHit = false;

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!isHit && rb.velocity != Vector3.zero)
        {
            transform.forward = rb.velocity.normalized;
        }
    }
    public void Init(Vector3 pos)
    {
        Vector3 screenPosition = UIManager.instance.crossHair.transform.position;
        screenPosition.z = Camera.main.farClipPlane;
        Vector3 bowForward = Camera.main.ScreenToWorldPoint(screenPosition);
        transform.position = pos;
        transform.forward = bowForward;
        rb.AddForce(transform.forward * power, ForceMode.Impulse);
    }
}
