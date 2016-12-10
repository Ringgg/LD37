using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    public static List<Danger> dangers = new List<Danger>();
    public float hp = 100;
    private float startHP;
    Movement movement;
    private HighlightColorChanger highlightColorChanger;

    public void Start()
    {
        startHP = hp;
        movement = GetComponent<Movement>();
        highlightColorChanger = GetComponentInChildren<HighlightColorChanger>();
    }

    public void Update()
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
                break;
            }
        }
    }

    public void TakeDamage(float ammount)
    {
        hp = Mathf.Max(0, hp - ammount);
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
        if (hp <= 0) Die();
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
