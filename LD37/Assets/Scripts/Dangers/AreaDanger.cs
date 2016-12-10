using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class AreaDanger : Danger
{
    public float damage = 20f;
    Hero tmp;

    public void OnTriggerEnter(Collider col)
    {
        tmp = col.GetComponent<Hero>();
        if (tmp == null) return;
        heroesIn.Add(tmp);
    }

    public void OnTriggerExit(Collider col)
    {
        tmp = col.GetComponent<Hero>();
        if (tmp == null) return;
        heroesIn.Remove(tmp);
    }

    protected void GiveHeroesDamage()
    {
        foreach (var hero in heroesIn)
        {
            if (hero == null) continue;
            hero.TakeDamage(damage);
        }
    }
}
