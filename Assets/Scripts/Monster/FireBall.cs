using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class FireBall : MonoBehaviour, IProjectile
{
    Rigidbody rb;
    Transform target;
    Transform attacker;
    float speed = 30f;

    Vector3 originScale;

    int damage = 3;

    private void Start()
    {
        originScale = transform.localScale;
        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        transform.localScale = originScale;
    }

    public void Init(Transform target, Vector3 pos, float duration)
    {
        this.target = target;
        transform.position = pos;
        StartCoroutine(Scale(duration));
        StartCoroutine(Shot(duration));
    }

    private IEnumerator Scale(float duration)
    {
        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = Vector3.one * 0.3f;

        float _duration = duration / 4;
        float elapsed = 0;
        while (elapsed <= _duration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsed / _duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = targetScale;
    }
    private IEnumerator Shot(float duration)
    {
        duration += 0.7f;
        yield return new WaitForSeconds(duration);
        Shoot();
    }
    public void Shoot()
    {
        if (target != null)
        {
            rb.velocity = (target.position - transform.position).normalized * speed;
        }
    }

    public void SetAttacker(Transform transform)
    {
        attacker = transform;
    }

    public Transform GetAttacker()
    {
        return attacker;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector3 hitDirection = collision.contacts[0].normal;

            hitDirection = -hitDirection.normalized;

            collision.gameObject.GetComponent<PlayerController>().Hit(hitDirection);
            collision.gameObject.GetComponent<IDamageable>().TakeDamage(damage);
        }
    }
}
