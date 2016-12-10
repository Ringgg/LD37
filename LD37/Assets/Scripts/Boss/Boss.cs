using UnityEngine;
using System.Collections.Generic;

public class Boss : MonoBehaviour
{
    public PhaseDefault curPhase;
    public PhaseDefault defaultPhase;
    public PhaseDefault healPhase;
    public PhaseDefault aoePhase;
    public Movement movement;
    
    public void Awake()
    {
        movement = GetComponent<Movement>();

        EventManager.StartListening(EventType.EndAoePhase, GoDefault);
    }

    void Update()
    {
        if (curPhase == defaultPhase)
        {
            if (Input.GetKeyDown(KeyCode.Q) && IsInDefault() && aoePhase.CanSwitch())
                GoExplosionSpam();
            if (Input.GetKeyDown(KeyCode.Q) && IsInDefault() && healPhase.CanSwitch())
                GoToPhase(healPhase);
        }
        
        if (Input.GetMouseButtonDown(0)) curPhase.LeftClick();
        if (Input.GetMouseButtonDown(1)) curPhase.RightClick();
    }

    public void GoDefault() { GoToPhase(defaultPhase); }
    public void GoExplosionSpam() { GoToPhase(aoePhase); }
    public void GoHeal() { GoToPhase(healPhase); }

    public void GoToPhase(PhaseDefault phase)
    {
        curPhase.EndPhase();
        curPhase = phase;
        curPhase.EndPhase();
    }

    public void WalkTo(Vector3 position)
    {
        movement.GoTo(position);
    }

    bool IsInDefault()
    {
        return curPhase = defaultPhase;
    }
}
