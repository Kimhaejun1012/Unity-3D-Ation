using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class SkillHandler : MonoBehaviour
{
    Coroutine coroutine;
    public Transform fireBallPos;
    public Transform arrowPos;

    GameObject curArrow;
    Action attackAction;
    [SerializeField] Transform targetPos;

    private void Start()
    {
    }

    public void DoFireballSkill()
    {
        //if (coroutine != null)
        //{
        //    return;
        //}
        coroutine = StartCoroutine(DoFireBallRoutine());
    }
    public void DoArrowAttack()
    {
        curArrow = ObjectPoolManager.instance.GetPool("MonsterArrow");
        curArrow.GetComponent<MonsterArrow>().Init(targetPos, arrowPos.position);
        attackAction = curArrow.GetComponent<MonsterArrow>().Shot;
    }
    public void ArrowShot()
    {
        attackAction.Invoke();
        curArrow = null;
    }
    private IEnumerator DoFireBallRoutine()
    {
        var fireball1 = ObjectPoolManager.instance.GetPool("FireBall");
        fireball1.GetComponent<FireBall>().Init(targetPos, fireBallPos.position);

        yield return new WaitForSeconds(1f);

        var fireball2 = ObjectPoolManager.instance.GetPool("FireBall");
        fireball2.GetComponent<FireBall>().Init(targetPos, fireBallPos.position + Vector3.Scale(Vector3.one, fireBallPos.right));

        yield return new WaitForSeconds(1f);

        var fireball3 = ObjectPoolManager.instance.GetPool("FireBall");
        fireball3.GetComponent<FireBall>().Init(targetPos, fireBallPos.position + Vector3.Scale(Vector3.one, -fireBallPos.right));
    }
}
