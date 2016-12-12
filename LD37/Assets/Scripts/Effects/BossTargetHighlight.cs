using UnityEngine;
using System.Collections;

public class BossTargetHighlight : MonoBehaviour
{
    Hero target;

    void Awake()
    {
        transform.parent = null;
    }

	void Update ()
    {
	    if (target == null)
        {
            Hide();
            return;
        }

        transform.position = target.transform.position;
        transform.Translate(Vector3.down * (transform.position.y - 0.05f), Space.World);
        transform.Rotate(0, Time.deltaTime * 180, 0, Space.World);
	}

    public void SetTarget(Hero target)
    {
        this.target = target;
    }

    public void Hide()
    {
        transform.position = new Vector3(0, -5, 0);
    }
}
