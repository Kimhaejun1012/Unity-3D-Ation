using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void SetTrigger(string name)
    {
        animator.SetTrigger(name);
    }
    public void SetBool(string name, bool value)
    {
        animator.SetBool(name, value);
    }
    public void SetFloat(string name, float x)
    {
        animator.SetFloat(name, x);
    }
    public bool GetBool(string name)
    {
        return animator.GetBool(name);
    }
    public void ResetTrigger(string name)
    {
        animator.ResetTrigger(name);
    }
    public bool IsAnimationRunning(string name)
    {
        if (animator != null)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName(name))
            {
                var normalizedTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

                return normalizedTime != 0 && normalizedTime < 1f;
            }
        }
        return false;
    }
    public void StartZoom()
    {
        GameManager.instance.CamZoomStart();
    }
    public void FinishZoom()
    {
        GameManager.instance.CamZoomFinish();
    }
}
