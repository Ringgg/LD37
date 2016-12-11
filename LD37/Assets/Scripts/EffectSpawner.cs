using UnityEngine;

public class EffectSpawner : MonoBehaviour
{
    static EffectSpawner instance;

    public GameObject dotCircle;
    public GameObject explosionCircle;

    public GameObject explosionParticle;

    public GameObject arrow;

    public static void SpawnDotCircle(Vector3 position)
    {
        Instantiate(instance.dotCircle, position, Quaternion.identity);
    }
    
    public static void SpawnExplosionCircle(Vector3 position)
    {
        Instantiate(instance.explosionCircle, position, Quaternion.identity);
    }
    
    public static void SpawnExplosionParticle(Vector3 position)
    {
        Instantiate(instance.explosionParticle, position, Quaternion.identity);
    }

    public static void SpawnArrow(Vector3 position)
    {
        Instantiate(instance.arrow, position, Quaternion.identity);
    }

    public static void SpawnHealthEffect(Vector3 position, Hero target)
    {

    }

    void Awake()
    {
        instance = this;
    }
}
