using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : ActorStats
{
    void Start()
    {
        UIManager.instance.HeartInit(maxHp);
    }
}
