using UnityEngine;

public class PhaseBase : MonoBehaviour
{
    protected Boss controller;
    protected Movement movement;

    public bool available;
    public bool active;

    public float cooldown = 60;
    public float duration = 15;

    public float cooldownTimer;
    public float durationTimer;

    protected Animator anim;

    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    protected virtual void Update()
    {
        if (active) durationTimer = Mathf.MoveTowards(durationTimer, 0, Time.deltaTime);
        else        cooldownTimer = Mathf.MoveTowards(cooldownTimer, 0, Time.deltaTime);
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

    public virtual void LeftClick() { }
    public virtual void RightClick() { }
}
