using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    Rigidbody rb;
    float timer = 0;
    Color startC, endC;

    public void Init(int Damage)
    {
        float multiplier = Random.Range(0.5f, 1.0f);

        startC = new Color(1, (multiplier > 0.9f ? 1f : 0.0f), 0, 1);
        endC = new Color(1, (multiplier > 0.9f ? 1f : 0.0f), 0, 0);
        if (multiplier > 0.9f)
            transform.localScale *= 1.2f;
        GetComponent<TextMesh>().color = new Color(multiplier, 0, 0);
        GetComponent<TextMesh>().text = ((int)(Damage * (multiplier + 0.25f))).ToString() + (multiplier > 0.9? "!" : "");

        rb = GetComponent<Rigidbody>();
        transform.position += Vector3.up * 2 + Random.insideUnitSphere*2;
        rb.AddForce((Vector3.up * 2 + Random.insideUnitSphere) * 3, ForceMode.VelocityChange);
        Invoke("DestroyMe", 1.0f);
    }

    void Update()
    {
        transform.LookAt(transform.position + Camera.main.transform.forward);
        timer += Time.deltaTime;
        GetComponent<TextMesh>().color = Color.Lerp(startC, endC, timer);
    }

    void DestroyMe()
    {
        Destroy(gameObject);
    }
}
