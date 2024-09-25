using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance = null;
    public Animator animator;

    float slowFactor = 0.3f;
    float dodgeSlowFactor = 0.1f;
    float slowMotionDuration = 0.5f;
    float transitionDuration = 1f;
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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            Time.timeScale = 0.3f;
        }

        if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            Time.timeScale = 1f;
        }
    }

    public void ApplySlowMotion()
    {
        Time.timeScale = slowFactor;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        AdjustAnimationSpeed();
    }
    public void ApplyDodgeSlowMotion()
    {
        Time.timeScale = dodgeSlowFactor;
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
    public void ParryingSlowMotion()
    {
        StartCoroutine(ParryingSlowMotionCoroutine());
    }
    private IEnumerator ParryingSlowMotionCoroutine()
    {
        float originalTimeScale = Time.timeScale;
        float originalFixedDeltaTime = Time.fixedDeltaTime;

        Time.timeScale = slowFactor;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        yield return null;

        Time.timeScale = slowFactor;

        yield return new WaitForSecondsRealtime(slowMotionDuration);
        float elapsedTime = 0;
        while (elapsedTime < transitionDuration)
        {
            Time.timeScale = Mathf.Lerp(slowFactor, originalTimeScale, elapsedTime / transitionDuration);
            Time.fixedDeltaTime = originalFixedDeltaTime * Time.timeScale;
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        Time.timeScale = originalTimeScale;
        Time.fixedDeltaTime = originalFixedDeltaTime;
    }
}
