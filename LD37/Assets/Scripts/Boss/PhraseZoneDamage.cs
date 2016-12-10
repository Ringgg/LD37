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

    public override void StartPhase()
    {
        base.StartPhase();
        AddMouseHovers();
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
        if (zoneActivateInRound) return;
        RaycastHit hitInfo;
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(r, out hitInfo))
            return;

        var zone = hitInfo.collider.gameObject;
        if (Zones.Contains(zone))
        {
            EnableComponent(zone, true);
            DestroyMouseHovers();
            zoneActivateInRound = true;
        }
    }



    void EnableComponent(GameObject zone, bool enabled)
    {
        if (zone.GetComponent<CooldownPlane>())
        {
            var cooldownPlane = zone.GetComponent<CooldownPlane>();
            cooldownPlane.enabled = enabled;
            if (enabled)
            {
                cooldownPlane.StartDamagingHeroes();
            }
            else
            {
                cooldownPlane.CancelInvoke();
            }
        }
        else if (zone.GetComponent<ExplosionPlane>())
        {
            var explosionPlane = zone.GetComponent<ExplosionPlane>();
            explosionPlane.enabled = enabled;
            if (!enabled) return;
            explosionPlane.GiveDamage();
            foreach (var particle in explosionPlane.GetComponentsInChildren<ParticleSystem>())
            {
                particle.Play();
            }

        }
        else if (zone.GetComponent<ExternalDanger>())
        {
            //foreach (var externalZone in Zones)
            //{
            //    if (!externalZone.GetComponent<ExternalDanger>()) continue;
                var externalDanger = zone.GetComponent<ExternalDanger>();
                externalDanger.enabled = enabled;
            //}

        }
    }

    private void AddMouseHovers()
    {
        foreach (var zone in Zones)
        {
            zone.AddComponent<MouseHover>();
        }
    }

    private void DestroyMouseHovers()
    {
        foreach (var zone in Zones)
        {
            zone.GetComponent<MouseHover>().ReturnToStartColor();
            Destroy(zone.GetComponent<MouseHover>());
        }
    }
}

