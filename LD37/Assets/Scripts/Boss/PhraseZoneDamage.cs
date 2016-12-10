using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PhraseZoneDamage : PhaseBase
{
    public List<GameObject> Zones;
    private bool zoneActivateInRound;
    void Update()
    {
        base.Update();
        if (active)
        {
            if (durationTimer == 0) EventManager.TriggerEvent(EventType.EndZonePhase);
        }
    }

    public override void EndPhase()
    {
        base.EndPhase();
        foreach (var zone in Zones)
        {
            EnableComponent(zone, false);
        }
        zoneActivateInRound = false;
    }

    public override void LeftClick()
    {
        if(zoneActivateInRound) return;
        RaycastHit hitInfo;
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(r, out hitInfo))
            return;

        var zone = hitInfo.collider.gameObject;
        if (Zones.Contains(zone))
        {
            EnableComponent(zone, true);
            zoneActivateInRound = true;
        }
    }

    void EnableComponent(GameObject zone, bool enabled)
    {
        if (zone.GetComponent<CooldownPlane>())
        {
            var cd = zone.GetComponent<CooldownPlane>();
            cd.enabled = enabled;
            if (enabled)
            {
                cd.StartDamagingHeroes();
            }
            else
            {
                cd.CancelInvoke();
            }
        }
        else if (zone.GetComponent<ExplosionPlane>())
        {
            zone.GetComponent<ExplosionPlane>().enabled = enabled;
            if (!enabled) return;
            zone.GetComponent<ExplosionPlane>().GiveDamage();
        }
    }
}

