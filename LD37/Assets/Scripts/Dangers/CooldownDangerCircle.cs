using System.Collections;
using UnityEngine;

public class CooldownDangerCircle : DangerCircle
{
    public float delay = .5f;

    void Awake()
    {
        InvokeRepeating("GiveDamageInRadius", 0, delay);
    }
}


