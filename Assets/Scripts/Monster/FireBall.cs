using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class FireBall : MonoBehaviour, IProjectile
{
    Rigidbody rb;
    Transform target;
    Transform attacker;
    float speed = 30f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(Scale());
        StartCoroutine(Shoot());
    }

    public void Init(Transform target, Vector3 pos)
    {
        this.target = target;
        transform.position = pos;
    }

    private IEnumerator Scale()
    {
        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = Vector3.one * 0.3f;
        float timer = 0;

        while (timer <= 2)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, timer);
            timer += Time.deltaTime;
            yield return null;
        }
        transform.localScale = targetScale;
    }
    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(3.3f);

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
}
