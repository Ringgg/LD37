using UnityEngine;

public class ExplosionPlane : AreaDanger
{
    public GameObject highlight;
    public ExplosionPlane leftPlane;
    public ExplosionPlane middlePlane;
    public ExplosionPlane rightPlane;

    private Vector3 leftEdge = new Vector3(-12, 0, 0);
    private Vector3 rightEdge = new Vector3(12, 0, 0);
    private float currentTimer;
    public bool enableBoom;
    public float explosionTimer = 5.0f;
    void Start()
    {
        currentTimer = explosionTimer;
    }    void Update()
    {
        if (!enableBoom)
        {
            CancelInvoke();
            return;
        }
        else
        {
            if (!IsInvoking())
                InvokeRepeating("Spawn", 0, 0.3f);
        }

        currentTimer = Mathf.MoveTowards(currentTimer, 0, Time.deltaTime);
        if (currentTimer == 0)
        {
            enableBoom = false;
            GiveHeroesDamage();
            currentTimer = explosionTimer;
            foreach (var particle in GetComponentsInChildren<ParticleSystem>())
            {
                particle.Play();
            }
            enabled = false;
        }    }

    public override bool IsInDanger(Hero hero)
    {
        return heroesIn.Contains(hero);
    }

    public override Vector3 GetEscapePosition(Hero hero)
    {
        Vector3 dest;        if (leftPlane.enableBoom && rightPlane.enableBoom)
        {
            dest = (Vector3.zero - hero.transform.position);
        }
        else if ((leftPlane.enableBoom && middlePlane.enableBoom) || leftPlane.enableBoom)
        {
            dest = rightEdge - hero.transform.position;
        }
        else if ((rightPlane.enableBoom && middlePlane.enableBoom) || rightPlane.enableBoom)
        {
            dest = leftEdge - hero.transform.position;
        }
        else
        {
            dest = rightEdge - hero.transform.position;
        }
        dest.y = hero.transform.position.y;
        return hero.transform.position + dest.normalized;
    }

    void OnDisable()
    {
        CancelInvoke();
        
        Hero.dangers.Remove(this);
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
