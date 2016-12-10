using UnityEngine;
using System.Collections.Generic;

public class Boss : MonoBehaviour
{
    BossPhase curPhase;
    BossPhase defaultPhase;
    BossPhase healPhase;
    BossPhase aoePhase;

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

    bool IsInDefault()
    {
        return curPhase = defaultPhase;
    }
}
