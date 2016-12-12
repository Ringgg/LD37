using UnityEngine;
using System.Collections;

public class HealHPController : MonoBehaviour
{
    public int hp;
    public int maxHP = 1000;
    private AudioSource audio;
    private bool destroyed;
    void Start()
    {
        hp = maxHP;
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (hp < 0 && !destroyed)
        {
            audio.Play();
            destroyed = true;
        }
        if (destroyed && !audio.isPlaying)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;
    }
}
