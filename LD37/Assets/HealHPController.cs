using UnityEngine;
using System.Collections;

public class HealHPController : MonoBehaviour
{
    public int hp;
    public int maxHP = 1000;
    private AudioSource audio;
    void Start()
    {
        hp = maxHP;
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (hp < 0)
        {
            AudioManager.instance.PlaySomeAudio(audio);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;
    }
}
