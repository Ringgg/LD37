using UnityEngine;
using System.Collections;

public class HealthRay : MonoBehaviour
{
    Hero healTarget;
    Transform target;
    Vector3 startPos;

    public float height = 3;
    public float flightTime = 0.5f;
    public float ammount = 5;
    float timer;

    Vector3 lastPos;

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        lastPos = transform.position;
        timer += Time.deltaTime;
        float progress = Mathf.Clamp(timer / flightTime, 0.0f, 1.0f);
        float h = 1 - (4 * (progress - 0.5f) * (progress - 0.5f));

        transform.position = Vector3.Lerp(startPos, target.position, progress);
        transform.position += Vector3.up * h * height;

        transform.LookAt(2 * transform.position - lastPos, Vector3.up);

        if (progress == 1.0f)
            TakeEffect();
    }

    public void Init(Hero ht)
    {
        startPos = transform.position;
        target = ht.transform;
        healTarget = ht;
    }

    void TakeEffect()
    {
        healTarget.TakeDamage(-ammount);
        EffectSpawner.SpawnHealthParticle(transform.position);
        Destroy(gameObject);
    }
}
