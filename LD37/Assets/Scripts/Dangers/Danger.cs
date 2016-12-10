using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Danger : MonoBehaviour
{
    public int priority;
    protected List<Hero> heroesIn = new List<Hero>();

    public virtual bool IsInDanger(Hero hero)
    {
        return false;
    }

    public virtual Vector3 GetEscapePosition(Hero hero)
    {
        return Vector3.zero;
    }

    void OnEnable()
    {
        Hero.AddDanger(this);
    }

    void OnDisable()
    {
        Hero.RemoveDanger(this);
    }
}
