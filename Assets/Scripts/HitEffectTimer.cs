using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect_Timer : MonoBehaviour
{
    public float delay = 3.0f;

    void OnEnable()
    {
        StartCoroutine(DeactivateAfterDelay());
    }

    private IEnumerator DeactivateAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        ObjectPoolManager.instance.ReturnPool("BlackHole_Hit", gameObject);
    }
}
