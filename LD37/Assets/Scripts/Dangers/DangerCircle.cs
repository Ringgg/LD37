using UnityEngine;
using System.Collections.Generic;

public class DangerCircle : Danger
{
    public float radius = 3;
    public float explosionDamage = 20f;

    List<Hero> heroesIn = new List<Hero>();
    Hero tmp;

    public override bool IsInDanger(Hero hero)
    {
        return Vector3.SqrMagnitude(transform.position - hero.transform.position) <= radius * radius;
    }

    public override Vector3 GetEscapePosition(Hero hero)
    {
        Vector3 dest = -(transform.position - hero.transform.position);
        dest.y = hero.transform.position.y;
        return dest.normalized * (radius + 0.5f);
    }


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

    protected void GiveDamageInRadius(float amount)
    {
        foreach (var hero in heroesIn)
        {
            if (hero == null) continue;
            hero.TakeDamage(amount);
        }
    }

    void OnDestroy()
    {
        GiveDamageInRadius(explosionDamage);
    }
}
