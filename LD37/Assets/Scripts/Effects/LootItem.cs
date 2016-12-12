using UnityEngine;
using System.Collections;

public class LootItem : MonoBehaviour
{
    Vector3 targetPos;
    Vector3 startPos;
    
    public float height = 3;
    public float flightTime = 0.5f;
    float timer;

    void Awake()
    {
        startPos = transform.position;
        targetPos = Random.insideUnitCircle * 4;
        targetPos.z = targetPos.y;
        targetPos.y = 0.1f;
        targetPos += transform.position;
    }

    void Update()
    {
        timer += Time.deltaTime;
        float progress = Mathf.Clamp(timer / flightTime, 0.0f, 1.0f);
        float h = 1 - (4 * (progress - 0.5f) * (progress - 0.5f));

        transform.position = Vector3.Lerp(startPos, targetPos, progress);
        transform.position += Vector3.up * h * height;
        
        if (progress == 1.0f)
        {
            GetComponentInChildren<ParticleSystem>().enableEmission = true;
            Vector3 c = Random.onUnitSphere;
            GetComponentInChildren<ParticleSystem>().startColor = new Color(c.x, c.y, c.z, 1);
            Destroy(this);
        }
    }
}
