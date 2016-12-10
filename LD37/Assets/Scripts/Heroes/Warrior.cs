using UnityEngine;
using System.Collections;

public class Warrior : Hero
{
    float minDist = 2f;
    float maxDist = 2.5f;

    public float damage = 10;
    public float strikeDelay = 1.0f;

    float strikeTimer;

    // helpers
    float distance;
    Vector3 dir;


    public void Start()
    {
        minDist *= Random.Range(0.9f, 1f);
        maxDist *= Random.Range(0.9f, 1f);
    }

    public override void Update()
    {
        base.Update();

        float distance = Vector3.Distance(transform.position, Boss.instance.transform.position);
        dir = (transform.position - Boss.instance.transform.position).normalized;

        strikeTimer = Mathf.MoveTowards(strikeTimer, 0.0f, Time.deltaTime);

        if (isInDanger)
            return;
        else if (distance > maxDist)
            movement.GoTo(Boss.instance.transform.position + dir * (maxDist - 0.5f));
        else if (distance < minDist)
            movement.GoTo(Boss.instance.transform.position + dir * (minDist + 0.5f));
        else if (movement.walking)
            return;
        else if (strikeTimer == 0.0f)
            Attack();
    }

    void Attack()
    {
        strikeTimer = strikeDelay * Random.Range(0.75f, 1.25f);

        Vector3 dir = (Boss.instance.transform.position - transform.position).normalized;
        Boss.instance.GetComponent<Rigidbody>().AddForce(dir, ForceMode.VelocityChange);
    }
}
