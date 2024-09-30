using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryingProjectile : MonoBehaviour
{
    Rigidbody rb;
    int damage = 3;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    float power = 10f;
    public void Init(Vector3 dir, Vector3 pos)
    {
        transform.position = pos;
        rb.AddForce(dir * power, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
            damageable?.TakeDamage(damage);
        }
        var effect = ObjectPoolManager.instance.GetPool("Parrying_Hit");
        effect.transform.position = other.ClosestPoint(transform.position);
        ObjectPoolManager.instance.ReturnPool("Parrying", gameObject);
    }
}
