using UnityEngine;
using UnityEngine.UI;

public class UIHeal : MonoBehaviour
{
    public Image Lfill, Rfill;
    PhaseHeal phase;

    public void Start()
    {
        EventManager.StartListening(EventType.StartAoePhase, Disable);
        EventManager.StartListening(EventType.StartDefaultPhase, Disable);
        EventManager.StartListening(EventType.StartGasPhase, Disable);
        EventManager.StartListening(EventType.StartHealPhase, Enable);
        EventManager.StartListening(EventType.StartZonePhase, Disable);
        Disable();

        phase = Boss.instance.healPhase as PhaseHeal;
    }

    public void Update()
    {
        if (Boss.instance == null)
            return;
        Lfill.fillAmount = 1;
        Rfill.fillAmount = 1 - phase.slamCdTimer / phase.slamCooldown;
    }

    public void Enable() { gameObject.SetActive(true); }
    public void Disable() { gameObject.SetActive(false); }
}
