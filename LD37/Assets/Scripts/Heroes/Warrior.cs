using UnityEngine;
using System.Collections;

public class Warrior : Hero
{
    float minDist = 2f;
    float maxDist = 2.5f;

    public int damage = 10;
    public float strikeDelay = 1.0f;

    float strikeTimer;

    // helpers
    float distance;
    Vector3 dir;

    private Vector3 targetPos;
    private PhaseHeal healPhase;
    private GameObject closestHealObject;


    public void Start()
    {
        minDist *= Random.Range(0.9f, 1f);
        maxDist *= Random.Range(0.9f, 1f);
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

            strikeTimer = Mathf.MoveTowards(strikeTimer, 0.0f, Time.deltaTime);

            if (isInDanger)
                return;
            else if (hp < 0.25f * startHP && distance < 8)
                movement.GoTo(targetPos + dir * 8.5f);
            else if (distance > maxDist)
                movement.GoTo(targetPos + dir * (maxDist - 0.5f));
            else if (distance < minDist)
                movement.GoTo(targetPos + dir * (minDist + 0.5f));
            else if (movement.walking)
                return;
            else if (strikeTimer == 0.0f)
                Attack();
        }
    }

    void Attack()
    {
        strikeTimer = strikeDelay * Random.Range(0.75f, 1.25f);
        if (healPhase.active)
        {
            AttackHealObject();
        }
        else
        {
            Boss.instance.TakeDamage(damage);
            Vector3 dir = (Boss.instance.transform.position - transform.position).normalized * 0.5f;
            Boss.instance.GetComponent<Rigidbody>().AddForce(dir, ForceMode.VelocityChange);
        }
    }

    private void AttackHealObject()
    {
        if (closestHealObject == null) return;
        closestHealObject.GetComponent<HealHPController>().TakeDamage(damage);
    }

    void SetTarget()
    {
        if (healPhase.active)
        {
            float minDistance = Mathf.Infinity;
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
        }
        else
        {
            targetPos = Boss.instance.transform.position;
        }
    }
}
