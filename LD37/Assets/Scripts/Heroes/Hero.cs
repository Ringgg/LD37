using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Hero : MonoBehaviour
{
    public static List<Hero> heroes = new List<Hero>();
    public static List<Danger> dangers = new List<Danger>();
    public float hp = 100;
    public float startHP;
    protected Movement movement;
    protected HighlightColorChanger highlightColorChanger;
    public bool isInDanger;


    public virtual void Awake()
    {
        startHP = hp;
        movement = GetComponent<Movement>();
        highlightColorChanger = GetComponentInChildren<HighlightColorChanger>();
        heroes.Add(this);
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
        highlightColorChanger.ChangeHighlightColor(hp,startHP);
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
        heroes.Remove(this);
        Destroy(gameObject);
    }

    public void GetThrown(float forTime = 1.0f)
    {
        movement.GetThrown(forTime);
    }
}
