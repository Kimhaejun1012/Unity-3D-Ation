using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour, IProjectile
{
    public bool isShoot = false;
    [SerializeField] float finalSize = 1;
    public float power = 30f;
    Rigidbody rb;

    public Transform attacker;
    public Transform target;
    Vector3 originScale;
    void Start()
    {
        originScale = transform.localScale;
    }
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnDisable()
    {
        transform.localScale = originScale;
    }
    public void Init(Transform target, Vector3 pos, float duration)
    {
        this.target = target;
        transform.position = pos;
        StartCoroutine(SetScale(duration));
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
    IEnumerator SetScale(float duration)
    {
        float elapsed = 0;
        Vector3 targetScale = new Vector3(finalSize, finalSize, finalSize);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.localScale = Vector3.Lerp(originScale, targetScale, elapsed / duration);
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
