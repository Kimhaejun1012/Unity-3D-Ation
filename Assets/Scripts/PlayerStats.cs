using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : ActorStats
{
    // Start is called before the first frame update
    void Start()
    {
        UIManager.instance.HeartInit(maxHp);
    }
}
