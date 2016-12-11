using UnityEngine;
using System.Collections;

public class ConstTranslate : MonoBehaviour
{
    public Vector3 speed;
    public bool local;
    
	void Update ()
    {
        transform.Translate(speed * Time.deltaTime, local ? Space.Self : Space.World);
	}
}
