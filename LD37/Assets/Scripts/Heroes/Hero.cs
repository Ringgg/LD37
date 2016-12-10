using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Hero : MonoBehaviour
{
    public static List<Danger> dangers = new List<Danger>();
    public float hp;
    Movement movement;

    public void Start()
    {
        movement = GetComponent<Movement>();
    }

    public void Update()
    {
        TestDangers();
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
}
