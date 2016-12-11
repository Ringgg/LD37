using UnityEngine;
using UnityEngine.UI;

public class UIPiss : MonoBehaviour
{
    public Image Lfill, Rfill;
    PhaseGas phase;

    public void Start()
    {
        EventManager.StartListening(EventType.StartAoePhase, Disable);
        EventManager.StartListening(EventType.StartDefaultPhase, Disable);
        EventManager.StartListening(EventType.StartGasPhase, Enable);
        EventManager.StartListening(EventType.StartHealPhase, Disable);
        EventManager.StartListening(EventType.StartZonePhase, Disable);
        Disable();

        phase = Boss.instance.gasPhase as PhaseGas;
    }

    public void Update()
    {
        if (Boss.instance == null)
            return;
        Lfill.fillAmount = 1 - phase.hitCdTimer / phase.hitCooldown;
        Rfill.fillAmount = 1 - phase.slamCdTimer / phase.slamCooldown;
    }

    public void Enable() { gameObject.SetActive(true); }
    public void Disable() { gameObject.SetActive(false); }
}
