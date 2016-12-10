using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CooldownPlane : ExplosionPlane
{
    public float delay = .5f;

    void Awake()
    {
        InvokeRepeating("GiveHeroesDamage", 0, delay);
    }
}
