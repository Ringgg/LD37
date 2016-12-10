using UnityEngine;
using System.Collections;

public class DancyBody : MonoBehaviour
{
    Transform par;
    Vector3 baseOffset;

    void Start()
    {
        par = transform.parent;
        baseOffset = transform.localPosition;
    }

    void Update()
    {
        Vector3 newPos = baseOffset +
            0.5f * new Vector3(
                Mathf.PerlinNoise(Time.time * 20, transform.position.x * 10),
                Mathf.PerlinNoise(Time.time * 20, transform.position.y * 10),
                Mathf.PerlinNoise(Time.time * 20, transform.position.z * 10));

        transform.localPosition = Vector3.Lerp(transform.localPosition, newPos, 0.01f);
    }
}
