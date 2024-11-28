using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    ParticleSystem particle;

    void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }

    void OnEnable()
    {
        particle.Play();
    }

    void Update()
    {
        if (!particle.isPlaying)
        {
            ObjectPoolManager.instance.ReturnPool("HitEffect", gameObject);
        }
    }
}
