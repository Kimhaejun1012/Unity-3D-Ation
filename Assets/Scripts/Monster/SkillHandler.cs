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
    [SerializeField] Transform attackerPos;

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
        curArrow = ObjectPoolManager.instance.GetPool("BlackHole");
        curArrow.GetComponent<BlackHole>().Init(targetPos, arrowPos.position);
        curArrow.GetComponent<IProjectile>().SetAttacker(attackerPos);
        attackAction = curArrow.GetComponent<BlackHole>().Shot;
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
        fireball1.GetComponent<IProjectile>().SetAttacker(attackerPos);
        yield return new WaitForSeconds(1f);

        var fireball2 = ObjectPoolManager.instance.GetPool("FireBall");
        fireball2.GetComponent<FireBall>().Init(targetPos, fireBallPos.position + Vector3.Scale(Vector3.one, fireBallPos.right));
        fireball2.GetComponent<IProjectile>().SetAttacker(attackerPos);

        yield return new WaitForSeconds(1f);

        var fireball3 = ObjectPoolManager.instance.GetPool("FireBall");
        fireball3.GetComponent<FireBall>().Init(targetPos, fireBallPos.position + Vector3.Scale(Vector3.one, -fireBallPos.right));
        fireball3.GetComponent<IProjectile>().SetAttacker(attackerPos);

    }
}
