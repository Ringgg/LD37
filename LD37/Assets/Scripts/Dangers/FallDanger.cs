using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class FallDanger : AreaDanger
{
    public override bool IsInDanger(Hero hero)
    {
        return heroesIn.Contains(hero);
    }

    public override Vector3 GetEscapePosition(Hero hero)
    {
        Vector3 dest = Vector3.zero - hero.transform.position;
        dest.y = hero.transform.position.y;
        return hero.transform.position + dest.normalized;
    }
}

