using UnityEngine;

public class CirleSizeChanger : MonoBehaviour
{
    private CooldownDangerCircle dangerCircle;
    private float scale;
    private float tmpScale;
    private float current;
    void Start()
    {
        dangerCircle = GetComponent<CooldownDangerCircle>();
    }

    void Update()
    {
        scale = Mathf.Clamp(scale + 25 * Time.deltaTime / (scale*scale + 1), 0.5f, 60f);
        transform.localScale = new Vector3(scale, transform.localScale.y, scale);

    }
}
