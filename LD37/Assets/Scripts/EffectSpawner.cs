using UnityEngine;

public class EffectSpawner : MonoBehaviour
{
    static EffectSpawner instance;

    public GameObject dotCircle;
    public GameObject explosionCircle;

    public static void SpawnDotCircle(Vector3 position)
    {
        Instantiate(instance.dotCircle, position, Quaternion.identity);
    }
    
    public static void SpawnExplosionCircle(Vector3 position)
    {
        Instantiate(instance.explosionCircle, position, Quaternion.identity);
    }

    void Awake()
    {
        instance = this;
    }
}
