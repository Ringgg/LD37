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
        //tmpScale = scale;
        //scale = dangerCircle.radius + Mathf.Lerp(current, dangerCircle.radius*.5f, Time.time / dangerCircle.radius);
        scale = Mathf.Clamp(scale + 10 * Time.deltaTime / (scale*scale + 1), 0.5f, 50f);
        transform.localScale = new Vector3(scale, transform.localScale.y, scale);

    }
}
