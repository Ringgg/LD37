using UnityEngine;
using System.Collections.Generic;

public class Boss : MonoBehaviour
{
    public static Boss instance;

    public PhaseBase curPhase;
    public PhaseBase defaultPhase;
    public PhaseBase healPhase;
    public PhaseBase zonePhase;
    public PhaseBase aoePhase;
    public PhaseBase gasPhase;
    public Movement movement;
    
    public void Awake()
    {
        instance = this;
        movement = GetComponent<Movement>();
    }

    void Start()
    {
        EventManager.StartListening(EventType.EndAoePhase, GoDefault);
        EventManager.StartListening(EventType.EndZonePhase, GoDefault);
        EventManager.StartListening(EventType.EndGasPhase, GoDefault);
        GoDefault();
    }

    void Update()
    {
        if (curPhase == defaultPhase)
        {
            if (Input.GetKeyDown(KeyCode.Q) && IsInDefault() && aoePhase.CanSwitch())
                GoExplosionSpam();
            if (Input.GetKeyDown(KeyCode.W) && IsInDefault() && healPhase.CanSwitch())
                GoToPhase(healPhase);
            if (Input.GetKeyDown(KeyCode.E) && IsInDefault() && zonePhase.CanSwitch())
                GoZone();
            if (Input.GetKeyDown(KeyCode.R) && IsInDefault() && gasPhase.CanSwitch())
                GoGas();
        }
        
        if (Input.GetMouseButtonDown(0)) curPhase.LeftClick();
        if (Input.GetMouseButtonDown(1)) curPhase.RightClick();
    }

    public void GoDefault() { GoToPhase(defaultPhase); }
    public void GoExplosionSpam() { GoToPhase(aoePhase); }
    public void GoHeal() { GoToPhase(healPhase); }
    public void GoZone() { GoToPhase(zonePhase); }
    public void GoGas() { GoToPhase(gasPhase); }

    public void GoToPhase(PhaseBase phase)
    {
        curPhase.EndPhase();
        curPhase = phase;
        curPhase.StartPhase();
    }

    public void WalkTo(Vector3 position)
    {
        movement.GoTo(position);
    }

    bool IsInDefault()
    {
        return curPhase == defaultPhase;
    }
}
