using UnityEngine;
using System.Collections;

public class ZonePhaseEffects : MonoBehaviour
{
    public GameObject highlight;

    void Start()
    {
        EventManager.StartListening(EventType.StartZonePhase, StartSpawning);
        EventManager.StartListening(EventType.EndZonePhase, EndSpawning);
    }

    void Spawn()
    {
        Instantiate(highlight, transform.position, transform.rotation);
    }

    void StartSpawning()
    {
        InvokeRepeating("Spawn", 0, 1.0f);
    }

    void EndSpawning()
    {
        CancelInvoke();
    }
}
