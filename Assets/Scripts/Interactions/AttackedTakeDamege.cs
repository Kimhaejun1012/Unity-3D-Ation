using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        var hpVar = stats.GetComponent<MonsterHpBar>();
        hpVar?.GetDamage(damage);
    }
}
