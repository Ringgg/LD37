using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CooldownPlane : ExplosionPlane
{
    public float delay = .5f;

    public void StartDamagingHeroes()
    {
        InvokeRepeating("GiveHeroesDamage", 0, delay);
    }
}
