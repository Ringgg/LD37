using UnityEngine;

public class Arrow : MonoBehaviour
{
    Transform target;
    Vector3 startPos;

    public float height = 3;
    public float flightTime = 0.5f;
    public int damage = 10;
    float timer;

    Vector3 lastPos;

	void Start ()
    {
        startPos = transform.position;
        target = Boss.instance.transform;
	}
	
	void Update ()
    {
        if (Boss.instance == null)
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
        {
            Boss.instance.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
