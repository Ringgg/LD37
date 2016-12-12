using System.Collections;
using UnityEngine;

public class CooldownDangerCircle : DangerCircle
{
    public float delay = .5f;
    private AudioSource audio;

    void Awake()
    {
        InvokeRepeating("GiveHeroesDamage", 0, delay);
        audio = GetComponent<AudioSource>();
        audio.Play();
    }

    void OnDestroy()
    {
        
    }
}


