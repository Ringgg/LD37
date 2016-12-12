using UnityEngine;

public class Arrow : MonoBehaviour
{
    Transform targetPos;
    private GameObject target;
    Vector3 startPos;

    private bool healMode;
    public float height = 3;
    public float flightTime = 0.5f;
    public int damage = 10;
    float timer;
    Vector3 lastPos;
    private AudioSource audio;

    void Awake()
    {
        startPos = transform.position;
        targetPos = Boss.instance.transform;
        audio = GetComponent<AudioSource>();
        audio.pitch = Random.Range(0.75f, 1.5f);
        audio.Play();
    }

    void Update()
    {
        if (Boss.instance == null || (target == null))
        {
            Destroy(gameObject);
            return;
        }

        lastPos = transform.position;
        timer += Time.deltaTime;
        float progress = Mathf.Clamp(timer / flightTime, 0.0f, 1.0f);
        float h = 1 - (4 * (progress - 0.5f) * (progress - 0.5f));

        transform.position = Vector3.Lerp(startPos, targetPos.position, progress);
        transform.position += Vector3.up * h * height;

        transform.LookAt(2 * transform.position - lastPos, Vector3.up);

        if (progress == 1.0f)
        {
            if (healMode)
            {
                AttackHealObject();
                Destroy(gameObject);
            }
            else
            {
                Boss.instance.TakeDamage(damage);
                EffectSpawner.SpawnDamageEffect(damage);
                Destroy(gameObject);
            }
        }
    }

    private void AttackHealObject()
    {
        if (target == null || target.GetComponent<HealHPController>() == null) return;
        target.GetComponent<HealHPController>().TakeDamage(damage);
    }

    public void SetTarget(GameObject closestHealObject, bool healPhaseActive)
    {
        if (closestHealObject == null && healPhaseActive)
        {
            target = null;
            targetPos = null;
            healMode = false;
        }
        else if (!healPhaseActive)
        {
            target = Boss.instance.gameObject;
            targetPos = Boss.instance.gameObject.transform;
            healMode = false;
        }
        else
        {
            target = closestHealObject;
            targetPos = closestHealObject.transform;
        }
        healMode = healPhaseActive;
    }
}
