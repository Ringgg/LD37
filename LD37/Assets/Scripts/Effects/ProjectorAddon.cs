using UnityEngine;
using System.Collections;

public class ProjectorAddon : MonoBehaviour
{
    Projector p;

	void Start ()
    {
        p = GetComponent<Projector>();
	}
	
	void Update ()
    {
        p.aspectRatio = transform.parent.localScale.x / transform.parent.localScale.z;
        p.orthographicSize = transform.parent.localScale.z / 2;

        transform.Rotate(0, Time.deltaTime * 60, 0, Space.World);
        transform.localPosition -= Vector3.up * Time.deltaTime;
	}
}
