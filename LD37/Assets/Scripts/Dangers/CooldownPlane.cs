using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CooldownPlane : AreaDanger
{
    public float delay = .5f;

    public ExplosionPlane secondPlane;

    public override bool IsInDanger(Hero hero)
    {
        return heroesIn.Contains(hero);
    }

    public override Vector3 GetEscapePosition(Hero hero)
    {
        Vector3 dest;
        if (secondPlane.isActiveAndEnabled)
        {
            dest = -(Vector3.zero - hero.transform.position);
        }
        else
        {
            dest = -(transform.position - hero.transform.position);
        }
        dest.y = hero.transform.position.y;
        return hero.transform.position + dest.normalized;
    }

    public void StartDamagingHeroes()
    {
        InvokeRepeating("GiveHeroesDamage", 0, delay);
    }
}
