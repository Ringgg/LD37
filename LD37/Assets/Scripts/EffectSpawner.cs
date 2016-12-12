using UnityEngine;

public class EffectSpawner : MonoBehaviour
{
    static EffectSpawner instance;

    public GameObject dotCircle;
    public GameObject explosionCircle;
    public GameObject gasCircle;
    public GameObject healObject;

    public GameObject explosionParticle;

    public GameObject healEffect;
    public GameObject healParticle;

    public GameObject arrow;
    public GameObject magic;
    public GameObject slamEffect;
    public GameObject dmgEffect;

    public GameObject lootRain;
    public GameObject loot;

    public static void SpawnDotCircle(Vector3 position)
    {
        Instantiate(instance.dotCircle, position, Quaternion.identity);
    }

    public static GameObject SpawnGasCircle(Vector3 position)
    {
        return Instantiate(instance.gasCircle, position, Quaternion.identity) as GameObject;
    }

    public static GameObject SpawnHealObject(Vector3 position)
    {
        return Instantiate(instance.healObject, position, Quaternion.identity) as GameObject;
    }

    public static void SpawnExplosionCircle(Vector3 position)
    {
        Instantiate(instance.explosionCircle, position, Quaternion.identity);
    }
    
    public static void SpawnExplosionParticle(Vector3 position)
    {
        Instantiate(instance.explosionParticle, position, Quaternion.identity);
    }

    public static GameObject SpawnArrow(Vector3 position)
    {
        return Instantiate(instance.arrow, position, Quaternion.identity) as GameObject;
    }

    public static GameObject SpawnMagicMissile(Vector3 position)
    {
        return Instantiate(instance.magic, position, Quaternion.identity) as GameObject;
    }

    public static void SpawnHealthEffect(Vector3 position, Hero target)
    {
        (Instantiate(instance.healEffect, position, Quaternion.identity) as GameObject).GetComponent<HealthRay>().Init(target);
    }

    public static void SpawnHealthParticle(Vector3 position)
    {
        Instantiate(instance.healParticle, position, Quaternion.identity);
    }

    public static void SpawnSlamEffect(Vector3 position, Quaternion rotation)
    {
        Instantiate(instance.slamEffect, position, rotation);
    }

    public static void SpawnDamageEffect(int damage)
    {
        (Instantiate(instance.dmgEffect, Boss.instance.transform.position, Quaternion.identity) as GameObject)
            .GetComponent<DamageIndicator>().Init(damage);
    }

    public static void SpawnLootRain()
    {
        Instantiate(instance.lootRain, Boss.instance.transform.position, Quaternion.identity);
    }

    public static void SpawnLoot(Vector3 position)
    {
        Instantiate(instance.loot, position, Quaternion.identity);
    }

    void Awake()
    {
        instance = this;
    }
}
