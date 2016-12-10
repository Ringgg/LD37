using System.Collections;
using UnityEngine;

public class CooldownDangerCircle : DangerCircle
{
    public float cooldownDamage = 5f;
    public float coroutineDelay = .5f;

    void Start()
    {
        StartCoroutine("GiveDamageWithDelay", coroutineDelay);
    }

    private IEnumerator GiveDamageWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            GiveDamageInRadius(cooldownDamage);
        }
    }
}


