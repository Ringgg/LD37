﻿using System.Collections.Generic;
using UnityEngine;

public class PhaseHeal : PhaseBase
{
    Hero _target;
    Hero target
    {
        get { return _target; }
        set
        {
            _target = value;
            Boss.instance.highlight.SetTarget(value);
        }
    }
    public float healObjectsAmount = 3;
    public float spawnDelay = 4f;
    public int HPBoost = 100000;
    public float slamRange = 6.0f;
    public float slamKnockback = 10.0f;
    public float slamCooldown = 5.0f;

    [HideInInspector]
    public float slamCdTimer;
    [HideInInspector]
    public List<GameObject> healObjects = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();
        controller = GetComponent<Boss>();
        movement = GetComponent<Movement>();
    }

    void Update()
    {
        base.Update();
        slamCdTimer = Mathf.MoveTowards(slamCdTimer, 0, Time.deltaTime);
        if (active)
        {
            if (durationTimer == 0)
            {
                EventManager.TriggerEvent(EventType.EndHealPhase);
            }
            if (target != null)
            {
                Chase();
                movement.stoppingDist = 2;
            }
            else
            {
                movement.stoppingDist = 0.5f;
            }
            CheckHealingDistance();
        }
    }

    public override void StartPhase()
    {
        base.StartPhase();
        PlayLaugh();
        InvokeRepeating("SpawnHealObjects", 0, spawnDelay);
    }


    public override void EndPhase()
    {
        base.EndPhase();
        CancelInvoke();
        DestroyHealObjects();
        healObjects.Clear();
    }

    // slam the ground in a 45 degree cone
    public override void RightClick()
    {
        float effect;

        if (slamCdTimer > 0)
            return;

        anim.SetTrigger("Shout");
        AudioManager.instance.SetClip(Boss.instance.audio, AudioManager.instance.bossSpecial);
        Boss.instance.audio.Play();
        Hero tmp = target;

        for (int i = Hero.heroes.Count - 1; i >= 0; --i)
        {
            target = Hero.heroes[i];

            if (target == null)
                continue;

            effect = 1 - Vector3.Distance(transform.position, target.transform.position) / slamRange;

            if (effect < 0.0f)
                continue;
            
            target.GetThrown(1.0f);

            Vector3 dir = (target.transform.position - transform.position).normalized + Vector3.up;
            target.GetComponent<Rigidbody>().AddForce(dir * (slamKnockback), ForceMode.Impulse);
        }

        target = tmp;
        slamCdTimer = slamCooldown;
    }

    void Chase()
    {
        movement.GoTo(target.transform.position);
    }

    public override void LeftClick()
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

    private void SpawnHealObjects()
    {
        if (healObjects.Count >= healObjectsAmount) return;
        Vector3 pos = new Vector3(Random.Range(-11f, 11f), 0.5f, Random.Range(-11f, 11f));
        healObjects.Add(EffectSpawner.SpawnHealObject(pos));
    }

    private void DestroyHealObjects()
    {
        foreach (var healObject in healObjects)
        {
            if (healObject == null) continue;
            Destroy(healObject);
        }
    }

    void CheckHealingDistance()
    {
        foreach (var healObject in healObjects)
        {
            if (healObject == null) continue;
            if (Vector3.Distance(transform.position, healObject.transform.position) < 2f)
            {
                controller.hp += HPBoost;
                if (controller.hp > controller.maxHp)
                {
                    controller.hp = controller.maxHp;
                }
                Destroy(healObject);
            }
        }
    }

}

