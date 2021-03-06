﻿using System.Collections.Generic;
using UnityEngine;

public class PhraseZoneDamage : PhaseBase
{
    public List<GameObject> Zones;
    public float zoneActivationDelay = 5f;
    public LayerMask layerMask;

    private float zoneActivationTimer;

    void Start()
    {
        controller = Boss.instance;
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
        
        if (active)
        {
            if (durationTimer == 0) EventManager.TriggerEvent(EventType.EndZonePhase);
            zoneActivationTimer = Mathf.MoveTowards(zoneActivationTimer, 0, Time.deltaTime);
            if (zoneActivationTimer == 0) EnableMouseHovers();
        }
    }

    public override void StartPhase()
    {
        base.StartPhase();
        PlayLaugh();
        anim.SetBool("Channel", true);
        EnableMouseHovers();
    }

    public override void EndPhase()
    {
        base.EndPhase();
        anim.SetBool("Channel", false);
        zoneActivationTimer = zoneActivationDelay;
        DisableMouseHovers();
    }

    public override void LeftClick()
    {
        if (zoneActivationTimer > 0) return;
        RaycastHit hitInfo;
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(r, out hitInfo, 1000 ,layerMask))
            return;
        
        var zone = hitInfo.collider.gameObject;
        if (Zones.Contains(zone))
        {
            DisableMouseHovers();
            if (EnableComponent(zone, true))
            {
                zoneActivationTimer = zoneActivationDelay;
            }
        }
    }

    bool EnableComponent(GameObject zone, bool enabled)
    {
        if (zone.GetComponent<ExplosionPlane>())
        {
            var explosionPlane = zone.GetComponent<ExplosionPlane>();
            if (enabled && explosionPlane.enabled) return false;
            explosionPlane.enabled = enabled;
            explosionPlane.enableBoom = enabled;
        }
        return true;
    }

    private void EnableMouseHovers()
    {
        foreach (var zone in Zones)
        {
            if (zone.GetComponent<ExplosionPlane>().enabled) continue;
            zone.GetComponent<MouseHover>().enabled = true;
        }
    }

    private void DisableMouseHovers()
    {
        foreach (var zone in Zones)
        {
            zone.GetComponent<MouseHover>().Deactivate();
            zone.GetComponent<MouseHover>().enabled = false;
        }
    }
}

