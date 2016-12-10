using UnityEngine;
using System.Collections;

public class BossPhase : MonoBehaviour
{
    public float cooldown;
    public float duration;

    public bool available;
    public bool active;

    float cooldownTimer;
    float durationTimer;
    
    protected virtual void Update()
    {
        if (active)
            durationTimer = Mathf.MoveTowards(durationTimer, 0, Time.deltaTime);
        else
            cooldownTimer = Mathf.MoveTowards(cooldownTimer, 0, Time.deltaTime);
    }

    public virtual bool CanSwitch()
    {
        return available && !active && cooldownTimer == 0;
    }

    public virtual void StartPhase()
    {
        active = true;
        cooldownTimer = cooldown;
        durationTimer = duration;
    }

    public virtual void EndPhase()
    {
        active = false;
    }

    public virtual void LeftClick()
    {

    }

    public virtual void RightClick()
    {

    }
}
