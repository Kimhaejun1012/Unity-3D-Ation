using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackedTakeDamege : MonoBehaviour, IDamageable
{
    ActorStats stats;
    private void Start()
    {
        stats = GetComponent<ActorStats>();
    }
    public void TakeDamage(int damage)
    {
        stats.HP -= damage;

        if(stats is PlayerStats)
        {
            UIManager.instance.UpdateHearts(stats.HP);
        }
        else
        {
            var hpVar = stats.GetComponent<MonsterHpBar>();
            hpVar?.GetDamage(damage);
        }
    }
}
