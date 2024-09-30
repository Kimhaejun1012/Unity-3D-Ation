using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    public Rigidbody rb;
    public float gravityScale = 1.0f;

    private Vector3 gravity;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        gravityScale = 1 / Time.timeScale;
    }

    void FixedUpdate()
    {
        gravity = Physics.gravity * gravityScale / Time.deltaTime;
        rb.AddForce(gravity, ForceMode.Acceleration);
    }
}