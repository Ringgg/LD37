using UnityEngine;
using System.Collections;

public class HealHPController : MonoBehaviour
{
    public int hp;
    public int maxHP = 1000;
    void Start()
    {
        hp = maxHP;
    }

    void Update()
    {
        if (hp < 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;
    }
}
