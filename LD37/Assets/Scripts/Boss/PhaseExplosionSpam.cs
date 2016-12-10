using UnityEngine;

public class PhaseExplosionSpam : PhaseBase
{
    public float explosionScatterRadius = 3.0f;
    public float explosionSpawnDelay = 0.2f;
    public Transform holdPositionPt;

    float spawnDelayTimer;
    bool readyToSpawn;

    void Awake()
    {
        controller = GetComponent<Boss>();
        movement = GetComponent<Movement>();
    }

    protected override void Update()
    {
        base.Update();
        if (active)
        {
            if (durationTimer == 0)
                EventManager.TriggerEvent(EventType.EndAoePhase);

            readyToSpawn = Vector3.Distance(transform.position, holdPositionPt.position) < 0.5f;

            if (!readyToSpawn)
            {
                movement.GoTo(holdPositionPt.position);
            }
            else
            {
                SpawnDelayedExplosions();
            }
        }
    }

    public override void StartPhase()
    {
        base.StartPhase();
        movement.stoppingDist = 0.5f;
        readyToSpawn = false;
    }

    public override void EndPhase()
    {
        base.EndPhase();
        readyToSpawn = false;
    }
    
    void SpawnDelayedExplosions()
    {
        spawnDelayTimer = Mathf.MoveTowards(spawnDelayTimer, 0, Time.deltaTime);

        if (spawnDelayTimer == 0.0f)
        {
            RaycastHit hitInfo;
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 point;

            if (!Physics.Raycast(r, out hitInfo))
                return;

            point = hitInfo.point + Random.insideUnitSphere * explosionScatterRadius;
            point.y = 0;

            EffectSpawner.SpawnExplosionCircle(point);
            spawnDelayTimer = explosionSpawnDelay;
        }
    }
}
