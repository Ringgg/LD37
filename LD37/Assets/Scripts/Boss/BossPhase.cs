using UnityEngine;
using System.Collections;

public class BossPhase : MonoBehaviour
{
    Boss controller;
    Movement movement;
    Hero target;

    public float baseKnockback = 2.0f;
    public float strongKnockback = 5.0f;

    public float baseDamage = 10.0f;
    public float strongDamage = 15.0f;

    public float cooldown = 60;
    public float duration = 15;
    public float hitCd = 2;

    public bool available;
    public bool active;
    public bool nextHitStrong;

    float cooldownTimer;
    float durationTimer;
    float hitCdTimer;

    void Awake()
    {
        controller = GetComponent<Boss>();
        movement = GetComponent<Movement>();
    }

    protected virtual void Update()
    {
        if (active)
        {
            durationTimer = Mathf.MoveTowards(durationTimer, 0, Time.deltaTime);
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
        else
            cooldownTimer = Mathf.MoveTowards(cooldownTimer, 0, Time.deltaTime);

    }

    public virtual bool CanSwitch()
    {
        return available && !active && cooldownTimer == 0;
    }

    public virtual void StartPhase()
    {
        target = null;
        active = true;
        cooldownTimer = cooldown;
        durationTimer = duration;
    }

    public virtual void EndPhase()
    {
        target = null;
        active = false;
    }

    public virtual void LeftClick()
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

    public virtual void RightClick()
    {

    }

    void Hit()
    {
        // play animation

        hitCdTimer = hitCd;
        target.GetThrown(1.0f);

        Vector3 dir = (target.transform.position - transform.position).normalized;
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
