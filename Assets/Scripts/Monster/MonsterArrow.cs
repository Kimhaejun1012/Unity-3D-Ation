using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterArrow : MonoBehaviour, IProjectile
{
    public bool isShoot = false;
    public float power = 30f;
    Rigidbody rb;

    public Transform attacker;
    public Transform target;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(SetScale());
    }
    public void Init(Transform target, Vector3 pos)
    {
        this.target = target;
        transform.position = pos;
    }

    void Update()
    {
        if (!isShoot)
        {
            transform.LookAt(target);
        }
    }
    public void Shot()
    {
        isShoot = true;
        rb.AddForce(transform.forward * power, ForceMode.Impulse);
    }
    IEnumerator SetScale()
    {
        float duration = 2.0f;
        float elapsed = 0;
        Vector3 initialScale = transform.localScale;
        Vector3 targetScale = new Vector3(5, 5, 5);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsed / duration);
            yield return null;
        }
        transform.localScale = targetScale;
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
