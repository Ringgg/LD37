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

    private Vector3 targetPos;
    private PhaseHeal healPhase;
    private GameObject closestHealObject;

    public void Start()
    {
        minDist *= Random.Range(0.75f, 1.25f);
        maxDist *= Random.Range(0.75f, 1.25f);
        healPhase = Boss.instance.GetComponent<PhaseHeal>();
    }

    public override void Update()
    {
        base.Update();

        if (Boss.instance != null)
        {
            SetTarget();
            float distance = Vector3.Distance(transform.position, targetPos);
            dir = (transform.position - targetPos).normalized;

            shotTimer = Mathf.MoveTowards(shotTimer, 0.0f, Time.deltaTime);

            if (isInDanger)
                return;
            else if (distance > maxDist)
                movement.GoTo(targetPos + dir * (maxDist - 0.5f));
            else if (distance < minDist)
                movement.GoTo(targetPos + dir * (minDist + 0.5f));
            else if (movement.walking)
                return;
            else if (shotTimer == 0.0f)
                Attack();
        }
    }

    void Attack()
    {
        if (!isMage)
        {
            var arrow = EffectSpawner.SpawnArrow(transform.position);
            arrow.GetComponent<Arrow>().SetTarget(closestHealObject, healPhase.active);
        }
        else
        {
            var missile = EffectSpawner.SpawnMagicMissile(transform.position);
            missile.GetComponent<Arrow>().SetTarget(closestHealObject, healPhase.active);
        }
        shotTimer = shotDelay * Random.Range(0.75f, 1.25f);
    }

    void SetTarget()
    {
        if (healPhase.active)
        {
            float minDistance = Mathf.Infinity;
            closestHealObject = null;
            foreach (var healObject in healPhase.healObjects)
            {
                if (healObject == null) continue;
                var distance = Vector3.Distance(healObject.transform.position, transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    targetPos = healObject.transform.position;
                    closestHealObject = healObject;
                }
            }
            if (closestHealObject == null)
            {
                targetPos = Boss.instance.transform.position;
            }
        }
        else
        {
            targetPos = Boss.instance.transform.position;
        }
    }
}
