using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    Vector3 bossPos;
    Vector3 heroesPos;
    Vector3 desPos;
    Vector3 tmp;

    Quaternion desRot;

    float minDist = 10f;

    void Start()
    {
        EventManager.StartListening(EventType.StartDefaultPhase, SpecialPhaseEnd);
        EventManager.StartListening(EventType.StartAoePhase, SpecialPhaseStart);
        EventManager.StartListening(EventType.StartGasPhase, SpecialPhaseStart);
        EventManager.StartListening(EventType.StartHealPhase, SpecialPhaseStart);
        EventManager.StartListening(EventType.StartZonePhase, SpecialPhaseStart);
    }

    void Update()
    {
        if (Boss.instance == null)
            return;
        tmp = Vector3.zero;
        foreach (Hero h in Hero.heroes)
            tmp += h.transform.position;

        heroesPos = tmp / Hero.heroes.Count;
        bossPos = Boss.instance.transform.position;

        tmp = heroesPos - bossPos;

        if (tmp.magnitude > minDist)
            desPos = Vector3.LerpUnclamped(bossPos, heroesPos, 1.5f) + Vector3.up * 3 * Mathf.Sqrt(tmp.magnitude);
        else
            desPos = tmp.normalized * minDist * 1.5f + bossPos + Vector3.up * 3 * Mathf.Sqrt(minDist * 1.5f);

        desRot = Quaternion.LookRotation(Boss.instance.transform.position - Vector3.up - transform.position);

        transform.position = Vector3.Lerp(transform.position, desPos, 0.01f);
        transform.rotation = Quaternion.Slerp(transform.rotation, desRot, 0.02f);

        //transform.LookAt(Boss.instance.transform.position - Vector3.up);
    }

    void SpecialPhaseStart()
    {
        minDist = 15;
    }

    void SpecialPhaseEnd()
    {
        minDist = 5;
    }
}
