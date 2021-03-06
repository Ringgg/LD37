﻿using UnityEngine;
using System.Collections.Generic;

public class DangerCircle : AreaDanger
{
    private float radius;

    private void Update()
    {
        radius = transform.GetComponent<Renderer>().bounds.extents.magnitude;
    }

    public override bool IsInDanger(Hero hero)
    {
        return Vector3.SqrMagnitude(transform.position - hero.transform.position) <= radius * radius;
    }

    public override Vector3 GetEscapePosition(Hero hero)
    {
        Vector3 dest = -(transform.position - hero.transform.position);
        dest.y = hero.transform.position.y;
        return hero.transform.position + dest.normalized * (radius + 0.5f);
    }  

    void OnDestroy()
    {
        GiveHeroesDamage();
        EffectSpawner.SpawnExplosionParticle(transform.position);
    }
}
