using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHpBar : MonoBehaviour
{
    [SerializeField] private Slider hpBar;
    private void Start()
    {
        int maxHp = GetComponent<ActorStats>().maxHp;
        hpBar.maxValue = maxHp;
        hpBar.value = maxHp;
    }
    public void GetDamage(int damage)
    {
        hpBar.value -= damage;

        if (hpBar.value < 0) { hpBar.value = 0; }
    }
}
