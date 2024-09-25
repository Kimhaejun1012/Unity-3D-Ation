using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorStats : MonoBehaviour
{
    public int maxHp;

    public int HP { get; set; }

    private void Awake()
    {
        HP = maxHp;
    }
}
