using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance = null;
    public Animator animator;
    float slowFactor = 0.3f;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    public void ApplySlowMotion()
    {
        Time.timeScale = slowFactor;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        AdjustAnimationSpeed();
    }
    void AdjustAnimationSpeed()
    {
        animator.speed = 1 / Time.timeScale;
    }
    public void SetTimeScaleOne()
    {
        Time.timeScale = 1;
        animator.speed = 1;
    }
}
