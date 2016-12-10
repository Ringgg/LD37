using UnityEngine;
using System.Collections.Generic;

public class Boss : MonoBehaviour
{
    public BossPhase curPhase;
    public BossPhase defaultPhase;
    public BossPhase healPhase;
    public BossPhase aoePhase;
    public Movement movement;
    
    public void Awake()
    {
        movement = GetComponent<Movement>();
    }

    void Update()
    {
        if (curPhase == defaultPhase)
        {
            if (Input.GetKeyDown(KeyCode.Q) && IsInDefault() && aoePhase.CanSwitch())
                GoToPhase(aoePhase);
            if (Input.GetKeyDown(KeyCode.Q) && IsInDefault() && healPhase.CanSwitch())
                GoToPhase(healPhase);
        }


        if (Input.GetMouseButtonDown(0)) curPhase.LeftClick();
        if (Input.GetMouseButtonDown(1)) curPhase.RightClick();
    }

    public void GoToPhase(BossPhase phase)
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
