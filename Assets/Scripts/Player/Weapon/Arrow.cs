using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Arrow : MonoBehaviour
{
    Rigidbody rb;
    TrailRenderer trailRenderer;
    public LayerMask monsterLayer;

    readonly float power = 100;

    int _damage;
    bool isHit = false;

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
        trailRenderer = GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        if (!isHit && rb.velocity != Vector3.zero)
        {
            transform.forward = rb.velocity.normalized;
        }
    }
    void OnEnable()
    {
        StartCoroutine(DeactivateAfterDelay());
    }

    private IEnumerator DeactivateAfterDelay()
    {
        yield return new WaitForSeconds(10f);
        ObjectPoolManager.instance.ReturnPool("Arrow", gameObject);
    }
    public void Init(Vector3 pos, int damage)
    {
        Vector3 screenPosition = UIManager.instance.crossHair.transform.position;
        screenPosition.z = Camera.main.farClipPlane;
        Vector3 bowForward = Camera.main.ScreenToWorldPoint(screenPosition);
        transform.position = pos;
        transform.forward = bowForward;
        trailRenderer.enabled = true;
        rb.AddForce(transform.forward * power, ForceMode.Impulse);
        _damage = damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Monster") && !isHit)
        {
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
            damageable?.TakeDamage(_damage);
            var effect = ObjectPoolManager.instance.GetPool("HitEffect");
            effect.transform.position = collision.contacts[0].point;
        }
        isHit = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Monster") && !isHit)
        {
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
            damageable?.TakeDamage(_damage);
            other.GetComponent<MonsterAI>().WeakHit(other);
            var effect = ObjectPoolManager.instance.GetPool("HitEffect");
            effect.transform.position = other.ClosestPoint(transform.position);
            isHit = true;
        }
        trailRenderer.enabled = false;
    }
}
