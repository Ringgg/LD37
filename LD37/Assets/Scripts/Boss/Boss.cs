using UnityEngine;
using System.Collections.Generic;

public class Boss : MonoBehaviour
{
    public static Boss instance;

    public PhaseBase curPhase;
    public PhaseBase defaultPhase;
    public PhaseBase healPhase;
    public PhaseBase zonePhase;
    public PhaseBase aoePhase;
    public PhaseBase gasPhase;
    public Movement movement;

    public int maxHp = 1000000;
    public int hp;

    private bool died;
    private bool started;

    Animator anim;
    Rigidbody rb;
    [HideInInspector]
    public AudioSource audio;
    public BossTargetHighlight highlight;

    public void Awake()
    {
        instance = this;
        movement = GetComponent<Movement>();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    void Start()
    {
        hp = maxHp;
        EventManager.StartListening(EventType.GameStart, GameStart);
        EventManager.StartListening(EventType.EndAoePhase, GoDefault);
        EventManager.StartListening(EventType.EndZonePhase, GoDefault);
        EventManager.StartListening(EventType.EndGasPhase, GoDefault);
        EventManager.StartListening(EventType.EndHealPhase, GoDefault);
        GoDefault();
    }

    void Update()
    {
        anim.SetFloat("Speed", rb.velocity.magnitude);
        if (curPhase == defaultPhase && started)
        {
            if (Input.GetKeyDown(KeyCode.Q) && IsInDefault() && aoePhase.CanSwitch())
                GoExplosionSpam();
            if (Input.GetKeyDown(KeyCode.W) && IsInDefault() && healPhase.CanSwitch())
                GoHeal();
            if (Input.GetKeyDown(KeyCode.E) && IsInDefault() && zonePhase.CanSwitch())
                GoZone();
            if (Input.GetKeyDown(KeyCode.R) && IsInDefault() && gasPhase.CanSwitch())
                GoGas();
        }
        
        if (Input.GetMouseButtonDown(0)) curPhase.LeftClick();
        if (Input.GetMouseButtonDown(1)) curPhase.RightClick();
    }

    public void GoDefault() { GoToPhase(defaultPhase); EventManager.TriggerEvent(EventType.StartDefaultPhase); }
    public void GoExplosionSpam() { GoToPhase(aoePhase); EventManager.TriggerEvent(EventType.StartAoePhase); }
    public void GoHeal() { GoToPhase(healPhase); EventManager.TriggerEvent(EventType.StartHealPhase); }
    public void GoZone() { GoToPhase(zonePhase); EventManager.TriggerEvent(EventType.StartZonePhase); }
    public void GoGas() { GoToPhase(gasPhase); EventManager.TriggerEvent(EventType.StartGasPhase); }

    public void GoToPhase(PhaseBase phase)
    {
        curPhase.EndPhase();
        curPhase = phase;
        curPhase.StartPhase();
    }

    bool IsInDefault()
    {
        return curPhase == defaultPhase;
    }

    public void WalkTo(Vector3 position)
    {
        movement.GoTo(position);
    }

    public void TakeDamage(int ammount)
    {
        hp = Mathf.Clamp(hp - ammount, 0, maxHp);
        if (hp == 0)
            Die();
    }

    public void Die()
    {
        EffectSpawner.SpawnLootRain();
        anim.SetTrigger("Die");
        AudioManager.instance.SetClip(audio,AudioManager.instance.bossDeath);
        audio.pitch = 1;
        AudioManager.instance.PlaySomeAudio(audio);
        anim.transform.parent = null;
        EventManager.TriggerEvent(EventType.BossDied, this);

        Vector3 pos;
        foreach(Hero h in Hero.heroes)
        {
            pos = Random.insideUnitSphere * 4.0f;
            pos.y = 0.0f;
            h.movement.GoTo(pos + transform.position);
        }
        Destroy(gameObject);
    }

    public void GameStart()
    {
        started = true;
    }
}
