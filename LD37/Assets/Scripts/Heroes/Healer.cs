using UnityEngine;
using System.Collections;

public class Healer : Hero
{
    float minDistBoss = 5;
    float maxDistBoss = 10;

    float minDistHealed = 3;
    float maxDistHealed = 5;

    public float damage = 10;
    public float shotDelay = 0.5f;

    float shotTimer;

    // helpers
    float distance;
    Vector3 dir;

    Hero healTarget;

    public void Start()
    {
        minDistBoss *= Random.Range(0.75f, 1.25f);
        maxDistBoss *= Random.Range(0.75f, 1.25f);
    }

    public override void Update()
    {
        base.Update();

        float distance = Vector3.Distance(transform.position, Boss.instance.transform.position);
        dir = (transform.position - Boss.instance.transform.position).normalized;

        shotTimer = Mathf.MoveTowards(shotTimer, 0.0f, Time.deltaTime);

        if (isInDanger)
            return;
        else if (distance > maxDistBoss)
            movement.GoTo(Boss.instance.transform.position + dir * (maxDistBoss - 0.5f));
        else if (distance < minDistBoss)
            movement.GoTo(Boss.instance.transform.position + dir * (minDistBoss + 0.5f));
        else if (movement.walking)
            return;
        else if (shotTimer == 0.0f)
            Attack();
    }

    void Attack()
    {
        EffectSpawner.SpawnArrow(transform.position);
        shotTimer = shotDelay * Random.Range(0.75f, 1.25f);
    }
}
