using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class DangerCircle : Danger
{
    public float radius = 3;
    public float explosionDamage = 20f;
    public float cooldownDamage = 5f;
    public float coroutineDelay = .5f;

    List<Hero> heroesIn = new List<Hero>();
    Hero tmp;

    void Start()
    {
        StartCoroutine("GiveDamageWithDelay", coroutineDelay);
    }

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
            col.GetComponent<Hero>().TakeDamage(20f);
        }
    }

    private void GiveDamageInRadius()
    {
        foreach (var hero in heroesIn)
        {
            if (hero == null) continue;
            hero.TakeDamage(cooldownDamage);
        }
    }

    private IEnumerator GiveDamageWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            GiveDamageInRadius();

        }
    }
}
