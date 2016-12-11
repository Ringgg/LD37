using UnityEngine;
using System.Collections;

public class Archer : Hero
{
    float minDist = 5;
    float maxDist = 10;
    
    public float shotDelay = 0.5f;
    public bool isMage;
    float shotTimer;

    // helpers
    float distance;
    Vector3 dir;
    

    public void Start()
    {
        minDist *= Random.Range(0.75f, 1.25f);
        maxDist *= Random.Range(0.75f, 1.25f);
    }

    public override void Update()
    {
        base.Update();
        
        if (Boss.instance != null)
        {
            float distance = Vector3.Distance(transform.position, Boss.instance.transform.position);
            dir = (transform.position - Boss.instance.transform.position).normalized;

            shotTimer = Mathf.MoveTowards(shotTimer, 0.0f, Time.deltaTime);

            if (isInDanger)
                return;
            else if (distance > maxDist)
                movement.GoTo(Boss.instance.transform.position + dir * (maxDist - 0.5f));
            else if (distance < minDist)
                movement.GoTo(Boss.instance.transform.position + dir * (minDist + 0.5f));
            else if (movement.walking)
                return;
            else if (shotTimer == 0.0f)
                Attack();
        }
    }

    void Attack()
    {
        if (!isMage)
            EffectSpawner.SpawnArrow(transform.position);
        else
            EffectSpawner.SpawnMagicMissile(transform.position);

        shotTimer = shotDelay * Random.Range(0.75f, 1.25f);
    }
}
