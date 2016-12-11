using UnityEngine;
using UnityEngine.UI;

public class UIDefault : MonoBehaviour
{
    public Image Lfill, Rfill;
    PhaseDefault phase;

    public void Start()
    {
        EventManager.StartListening(EventType.StartAoePhase, Disable);
        EventManager.StartListening(EventType.StartDefaultPhase, Enable);
        EventManager.StartListening(EventType.StartGasPhase, Disable);
        EventManager.StartListening(EventType.StartHealPhase, Disable);
        EventManager.StartListening(EventType.StartZonePhase, Disable);

        phase = Boss.instance.defaultPhase as PhaseDefault;
    }

    public void Update()
    {
        if (Boss.instance == null)
            return;
        Lfill.fillAmount = 1 - phase.hitCdTimer / phase.hitCd;
        Rfill.fillAmount = 1 - phase.slamCdTimer / phase.slamCd;
    }

    public void Enable() { gameObject.SetActive(true); }
    public void Disable() { gameObject.SetActive(false); }
}
