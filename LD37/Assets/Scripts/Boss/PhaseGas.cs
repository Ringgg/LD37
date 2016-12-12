using System.Collections.Generic;
using UnityEngine;

public class PhaseGas : PhaseBase
{
    Hero _target;
    Hero target
    {
        get { return _target; }
        set
        {
            _target = value;
            Boss.instance.highlight.SetTarget(value);
        }
    }
    [HideInInspector]
    public float hitCdTimer;
    public float gasSpawnDelay = 3f;
    public float baseKnockback = 2.0f;
    public float baseDamage = 10.0f;
    public float hitCooldown = 2;

    public float slamRange = 6.0f;
    public float slamKnockback = 10.0f;
    public float slamCooldown = 5.0f;
    [HideInInspector]
    public float slamCdTimer;

    private List<GameObject> gasCircles = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();
        movement = GetComponent<Movement>();
    }

    protected override void Update()
    {
        base.Update();
        hitCdTimer = Mathf.MoveTowards(hitCdTimer, 0, Time.deltaTime);
        slamCdTimer = Mathf.MoveTowards(slamCdTimer, 0, Time.deltaTime);
        if (active)
        {
            if (durationTimer == 0)
            {
                EventManager.TriggerEvent(EventType.EndGasPhase);
            }
            if (target != null)
            {
                Chase();
                movement.stoppingDist = 2;
                if (CanHit())
                    Hit();
            }
            else
            {
                movement.stoppingDist = 0.5f;
            }
        }
    }

    public override void StartPhase()
    {
        base.StartPhase();
        InvokeRepeating("SpawnGasCircle", 0, gasSpawnDelay);
    }

    public override void EndPhase()
    {
        base.EndPhase();
        CancelInvoke();
        foreach (var gasCircle in gasCircles)
        {
            if(gasCircle == null) continue;
            Destroy(gasCircle);
        }
    }

    public override void LeftClick()
    {
        RaycastHit hitInfo;
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(r, out hitInfo))
            return;

        target = hitInfo.collider.GetComponent<Hero>();

        if (target == null)
        {
            movement.stoppingDist = 0.5f;
            movement.GoTo(hitInfo.point);
        }
    }

    void Hit()
    {
        anim.SetTrigger("Attack");

        hitCdTimer = hitCooldown;
        target.GetThrown(1.0f);

        Vector3 dir = (target.transform.position - transform.position).normalized + Vector3.up;
        target.TakeDamage(baseDamage);
        target.GetComponent<Rigidbody>().AddForce(dir * (baseKnockback), ForceMode.Impulse);
    }

    void Chase()
    {
        movement.GoTo(target.transform.position);
    }

    bool CanHit()
    {
        return
            target != null &&
            hitCdTimer == 0.0f &&
            Vector3.Distance(transform.position, target.transform.position) < 2.0f;
    }

    public override void RightClick()
    {
        float effect;

        if (slamCdTimer > 0)
            return;

        anim.SetTrigger("Shout");
        Hero tmp = target;

        for (int i = Hero.heroes.Count - 1; i >= 0; --i)
        {
            target = Hero.heroes[i];

            if (target == null)
                continue;

            effect = 1 - Vector3.Distance(transform.position, target.transform.position) / slamRange;

            if (effect < 0.0f)
                continue;

            target.GetThrown(1.0f);

            Vector3 dir = -(target.transform.position - transform.position).normalized + (Vector3.up * .5f);
            target.GetComponent<Rigidbody>().AddForce(dir * (slamKnockback), ForceMode.Impulse);
        }

        target = tmp;
        slamCdTimer = slamCooldown;
    }

    private void SpawnGasCircle()
    {
        Vector3 position = transform.position;
        position.y = 0;
        gasCircles.Add(EffectSpawner.SpawnGasCircle(position));
    }

}

