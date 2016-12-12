using UnityEngine;
using System.Collections;

public class PhaseDefault : PhaseBase
{
    Hero target;

    public float slamRange = 6.0f;
    public float slamKnockback = 20.0f;
    public float slamDamage = 80.0f;
    public float slamCd = 10.0f;

    public float baseKnockback = 2.0f;
    public float baseDamage = 10.0f;
    public float hitCd = 2;

    public bool nextHitStrong;

    [HideInInspector]
    public float hitCdTimer;
    [HideInInspector]
    public float slamCdTimer;
    
    protected override void Awake()
    {
        base.Awake();
        controller = GetComponent<Boss>();
        movement = GetComponent<Movement>();
        anim = GetComponentInChildren<Animator>();
    }

    protected override void Update()
    {
        base.Update();
        hitCdTimer = Mathf.MoveTowards(hitCdTimer, 0, Time.deltaTime);
        slamCdTimer = Mathf.MoveTowards(slamCdTimer, 0, Time.deltaTime);
        if (active)
        {

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
        target = null;
    }

    public override void EndPhase()
    {
        base.EndPhase();
        target = null;
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

    // slam the ground in a 45 degree cone
    public override void RightClick()
    {
        float effect;

        if (slamCdTimer > 0)
            return;

        anim.SetTrigger("Slam");
        //movement.Halt();
        Hero tmp = target;

        for (int i = Hero.heroes.Count -1; i >= 0; --i)
        {
            target = Hero.heroes[i];

            if (target == null)
                continue;

            effect = 1 - Vector3.Distance(transform.position, target.transform.position) / slamRange;

            if (effect < 0.0f)
                continue;

            if (Vector3.Dot(transform.forward, (target.transform.position - transform.position).normalized) < 0.5f)
                continue;

            target.GetThrown(1.0f);

            Vector3 dir = (target.transform.position - transform.position).normalized + Vector3.up;
            target.TakeDamage(slamDamage);
            target.GetComponent<Rigidbody>().AddForce(dir * (slamKnockback), ForceMode.Impulse);
        }

        target = tmp;
        EffectSpawner.SpawnSlamEffect(transform.position, transform.rotation);
        
        slamCdTimer = slamCd;
    }

    void Hit()
    {
        anim.SetTrigger("Attack");

        hitCdTimer = hitCd;
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
}
