using UnityEngine;
using UnityEngine.UI;

public class UIBoom : MonoBehaviour
{
    public Image Lfill, Rfill;
    PhraseZoneDamage phase;

    public void Start()
    {
        EventManager.StartListening(EventType.StartAoePhase, Disable);
        EventManager.StartListening(EventType.StartDefaultPhase, Disable);
        EventManager.StartListening(EventType.StartGasPhase, Disable);
        EventManager.StartListening(EventType.StartHealPhase, Disable);
        EventManager.StartListening(EventType.StartZonePhase, Enable);
        Disable();

        phase = Boss.instance.zonePhase as PhraseZoneDamage;
    }

    public void Update()
    {
        if (Boss.instance == null)
            return;
        Lfill.fillAmount = 1;
        Rfill.fillAmount = 1;
    }

    public void Enable() { gameObject.SetActive(true); }
    public void Disable() { gameObject.SetActive(false); }
}
