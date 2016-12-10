using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    public Vector3 destination;
    public float speed = 5;
    public float stoppingDist = 0.5f;
    public bool walking;
    public bool stopping;
    public bool thrown;

    Rigidbody rb;

    // helpers
    public Vector3 dir;
    float desiredSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //if (name != "Boss")
        //    InvokeRepeating("GoTo", 0, 3);
    }

    void FixedUpdate()
    {
        if (thrown) Fly();
        else
        {
            if (walking) Walk();
            if (stopping) Stop();
        }
    }

    public void Fly()
    {
        rb.velocity = Vector3.MoveTowards(rb.velocity, Vector3.zero, speed * Time.deltaTime);
        stopping = false;
        if (rb.velocity.sqrMagnitude < 0.01f)
            thrown = false;
    }

    public void Stop()
    {
        rb.velocity = Vector3.MoveTowards(rb.velocity, Vector3.zero, speed * Time.deltaTime * 4);
    }

    public void Walk()
    {
        dir = (destination - transform.position);

        if (dir.sqrMagnitude < stoppingDist * stoppingDist)
        {
            walking = false;
            stopping = true;
        }

        dir.Normalize();

        if (destination.sqrMagnitude < 2)
            desiredSpeed = Mathf.MoveTowards(speed, 0, Mathf.Max(1 - destination.magnitude, 0));
        else
            desiredSpeed = speed;
        dir *= desiredSpeed;
        dir.y = rb.velocity.y;

        rb.velocity = Vector3.MoveTowards(rb.velocity, dir, speed * Time.deltaTime * 2);

        Rotate();
    }

    void Rotate()
    {
        dir.y = transform.position.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir, Vector3.up), Time.deltaTime * 5);
    }

    public void GoTo(Vector3 position)
    {
        destination = position;
        walking = true;
        stopping = false;
    }

    public void GoTo()
    {
        Vector3 position = new Vector3(Random.Range(-10, 10.0f), transform.position.y, Random.Range(-10, 10.0f));
        destination = position;
        walking = true;
        stopping = false;
    }

    public void GetThrown(float forTime)
    {
        thrown = true;
        rb.drag = 1.0f;
        Invoke("Unthrow", forTime);
    }

    void Unthrow()
    {
        thrown = false;
    }
}
