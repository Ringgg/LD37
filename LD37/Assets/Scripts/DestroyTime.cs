using UnityEngine;
using System.Collections;

public class DestroyTime : MonoBehaviour
{

    public float destroyTime = 5.0f;

	void Start ()
    {
        Invoke("DestroyMe", destroyTime);
	}
	
    void DestroyMe()
    {
        Destroy(gameObject);
    }
}
