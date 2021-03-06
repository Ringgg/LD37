﻿using UnityEngine;
using System.Collections;

public class Healer : Hero
{
    float minDistBoss = 5;
    float maxDistBoss = 20;

    // todo podpiac te 2 zmienne
    float minDistHealed = 2;
    float maxDistHealed = 5;

    public float healAmmount = 5;
    public float shotDelay = 0.5f;

    float shotTimer;

    // helpers
    float distance;
    Vector3 dir;

    public Hero healTarget;

    public void Start()
    {
        minDistBoss *= Random.Range(0.75f, 1.25f);
        maxDistBoss *= Random.Range(0.75f, 1.25f);
    }

    public override void Update()
    {
        base.Update();

        if (Boss.instance != null)
        {
            float distance = Vector3.Distance(transform.position, Boss.instance.transform.position);
            dir = (transform.position - Boss.instance.transform.position).normalized;

            shotTimer = Mathf.MoveTowards(shotTimer, 0.0f, Time.deltaTime);

            if (isInDanger)
                return;
            else if (distance > maxDistBoss)
                movement.GoTo(Boss.instance.transform.position + dir * (maxDistBoss - 0.5f));
            else if (distance < minDistBoss)
                movement.GoTo(Boss.instance.transform.position + dir * (minDistBoss + 0.5f));
            else if (movement.walking || movement.thrown)
                return;
            else if (healTarget == null)
                LookForTarget();
            else
                Heal();
        }
        else if (movement.walking || movement.thrown)
            return;
        else if (healTarget == null)
            LookForTarget();
        else
            Heal();
    }

    void LookForTarget()
    {
        int id;
        for (int i = 0; i < 12; ++i)
        {
            id = Random.Range(0, heroes.Count);
            if (heroes[id].hp < heroes[id].startHP - 1)
            {
                healTarget = heroes[id];
                return;
            }
        }
    }

    void Heal()
    {
        if (healTarget.hp == healTarget.startHP)
        {
            healTarget = null;
            return;
        }

        if (shotTimer > 0)
            return;
        AudioManager.instance.SetClip(audio, AudioManager.instance.healSound);
        audio.Play();
        EffectSpawner.SpawnHealthEffect(transform.position, healTarget);
        shotTimer = shotDelay;
    }
}
