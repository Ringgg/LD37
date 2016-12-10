using UnityEngine;
using System.Collections;

public class EffectSpawner : MonoBehaviour
{
    static EffectSpawner instance;

    public GameObject dotCircle;

    public float interval = 2;

    public static void SpawnDotCircle (Vector3 position)
    {
        Instantiate(instance.dotCircle, position, Quaternion.identity);
    }    

    void Awake()
    {
        instance = this;
        InvokeRepeating("Spawn", 0, interval);
    }

    void Spawn()
    {
        SpawnDotCircle(new Vector3(Random.Range(-7, 7.0f), transform.position.y, Random.Range(-7, 7.0f)));
    }
}
