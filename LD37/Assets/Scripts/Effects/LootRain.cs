using UnityEngine;
using System.Collections;

public class LootRain : MonoBehaviour
{
	void Start ()
    {
        InvokeRepeating("Spam", 0, 0.15f);
	}

    void Spam()
    {
        EffectSpawner.SpawnLoot(transform.position);
    }
}
