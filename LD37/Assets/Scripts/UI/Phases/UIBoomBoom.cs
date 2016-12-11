using UnityEngine;
using UnityEngine.UI;

public class UIBoomBoom : MonoBehaviour
{
    public Image Lfill, Rfill;
    PhaseExplosionSpam phase;

    public void Start()
    {
        EventManager.StartListening(EventType.StartAoePhase, Enable);
        EventManager.StartListening(EventType.StartDefaultPhase, Disable);
        EventManager.StartListening(EventType.StartGasPhase, Disable);
        EventManager.StartListening(EventType.StartHealPhase, Disable);
        EventManager.StartListening(EventType.StartZonePhase, Disable);
        Disable();

        phase = Boss.instance.aoePhase as PhaseExplosionSpam;
    }

    public void Update()
    {
        if (Boss.instance == null)
            return;
        Lfill.fillAmount = 1 - phase.spawnDelayTimer / phase.explosionSpawnDelay;
        Rfill.fillAmount = 1 - phase.spawnDelayTimer / phase.explosionSpawnDelay;
    }

    public void Enable() { gameObject.SetActive(true); }
    public void Disable() { gameObject.SetActive(false); }
}
