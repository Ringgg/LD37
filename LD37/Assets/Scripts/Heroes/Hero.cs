using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Hero : MonoBehaviour
{
    public static List<Hero> heroes = new List<Hero>();
    public static List<Danger> dangers = new List<Danger>();
    public float hp = 100;
    public float startHP;
    public Movement movement;
    public bool isInDanger;
    protected AudioSource audio;

    private bool died;


    public virtual void Awake()
    {
        startHP = hp;
        movement = GetComponent<Movement>();
        heroes.Add(this);
        audio = GetComponent<AudioSource>();
    }

    public virtual void Update()
    {
        TestDangers();
        CheckIfAlive();
    }

    void TestDangers()
    {
        for (int i = 0; i < dangers.Count; ++i)
        {
            if (dangers[i].IsInDanger(this))
            {
                movement.GoTo(dangers[i].GetEscapePosition(this));
                isInDanger = true;
                return;
            }
        }
        isInDanger = false;
    }

    public void TakeDamage(float ammount)
    {
        hp = Mathf.Clamp(hp - ammount, 0, startHP);
    }
    
    public static void AddDanger(Danger danger)
    {
        dangers.Add(danger);
        dangers.OrderByDescending(x => x.priority);
    }

    public static void RemoveDanger(Danger danger)
    {
        dangers.Remove(danger);
    }

    private void CheckIfAlive()
    {
        if (hp <= 0 || transform.position.y < -100) Die();
    }

    public void Die()
    {
        if (died && !audio.isPlaying)
        {
           Destroy(gameObject);
           return; 
        }
        if(died) return;
        AudioManager.instance.SetRandomClipFromList(audio,AudioManager.instance.dieClips);
        Debug.Log(audio.clip);
        audio.Play();
        heroes.Remove(this);
        if (heroes.Count == 0)
            EventManager.TriggerEvent(EventType.HeroesKilled);
        died = true;
    }

    public void GetThrown(float forTime = 1.0f)
    {
        movement.GetThrown(forTime);
    }
}
