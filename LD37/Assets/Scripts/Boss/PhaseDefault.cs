using UnityEngine;
using System.Collections;

public class PhaseDefault : PhaseBase
{
    Hero target;

    public float baseKnockback = 2.0f;
    public float strongKnockback = 5.0f;

    public float baseDamage = 10.0f;
    public float strongDamage = 15.0f;
    
    public float hitCd = 2;

    public bool nextHitStrong;

    float hitCdTimer;

    void Awake()
    {
        controller = GetComponent<Boss>();
        movement = GetComponent<Movement>();
    }

    protected override void Update()
    {
        base.Update();
        if (active)
        {
            hitCdTimer = Mathf.MoveTowards(hitCdTimer, 0, Time.deltaTime);
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

    void Hit()
    {
        // play animation

        hitCdTimer = hitCd;
        target.GetThrown(1.0f);

        Vector3 dir = (target.transform.position - transform.position).normalized + Vector3.up;
        target.TakeDamage(nextHitStrong ? strongDamage : baseDamage);
        target.GetComponent<Rigidbody>().AddForce(dir * (nextHitStrong ? strongKnockback : baseKnockback), ForceMode.Impulse);
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
