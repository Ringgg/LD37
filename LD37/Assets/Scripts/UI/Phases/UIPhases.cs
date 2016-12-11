using UnityEngine;
using UnityEngine.UI;

public class UIPhases : MonoBehaviour
{
    public Image fill1, fill2, fill3, fill4;
    PhaseBase phase1, phase2, phase3, phase4;

    public void Start()
    {
        phase1 = Boss.instance.aoePhase;
        phase2 = Boss.instance.healPhase;
        phase3 = Boss.instance.zonePhase;
        phase4 = Boss.instance.gasPhase;
    }

    public void Update()
    {
        if (Boss.instance == null)
            return;

        if (phase1.active)  fill1.fillAmount = phase1.durationTimer / phase1.duration;
        else                fill1.fillAmount = 1 - phase1.cooldownTimer / phase1.cooldown;

        if (phase2.active)  fill2.fillAmount = phase2.durationTimer / phase2.duration;
        else                fill2.fillAmount = 1 - phase2.cooldownTimer / phase2.cooldown;

        if (phase3.active)  fill3.fillAmount = phase3.durationTimer / phase3.duration;
        else                fill3.fillAmount = 1 - phase3.cooldownTimer / phase3.cooldown;

        if (phase4.active)  fill4.fillAmount = phase4.durationTimer / phase4.duration;
        else                fill4.fillAmount = 1 - phase4.cooldownTimer / phase4.cooldown;

    }
}
