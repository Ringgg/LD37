using UnityEngine;
using System.Collections.Generic;

public class DangerCircle : Danger
{
    public float radius = 3;

    List<Hero> heroesIn = new List<Hero>();
    Hero tmp;

    void Update()
    {

    }

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
        if (col.GetComponent<Hero>())
        {

        }
    }
}
