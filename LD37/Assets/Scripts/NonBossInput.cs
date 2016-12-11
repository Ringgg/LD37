using UnityEngine;
using System.Collections;

public class NonBossInput : MonoBehaviour
{
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
	}
}
