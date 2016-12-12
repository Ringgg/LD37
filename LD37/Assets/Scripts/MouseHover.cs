using UnityEngine;

public class MouseHover : MonoBehaviour
{
    public GameObject trasformablePlane;

    private Vector3 startPos;
    private Renderer renderer;
    private bool enableHovering;

    float freq = 0.75f;
    float freqTimer;
    Vector3 startScale;

    void Awake()
    {
        startPos = trasformablePlane.transform.position;
        startScale = trasformablePlane.transform.localScale;
        renderer = trasformablePlane.GetComponent<Renderer>();
        renderer.enabled = false;
    }

    void Update()
    {
        if (!enableHovering) return;
        renderer.enabled = true;
        freqTimer = (freqTimer + Time.deltaTime) % freq;
        trasformablePlane.transform.position = trasformablePlane.transform.position + Vector3.up * 0.004123f;
        trasformablePlane.transform.localScale = startScale * (0.25f + 0.75f * freqTimer / freq);
    }

    void OnMouseEnter()
    {
        enableHovering = true;
    }
    void OnMouseExit()
    {
        Deactivate();
    }

    public void ReturnToStartPosition()
    {
        trasformablePlane.transform.position = startPos;
    }

    public void Deactivate()
    {
        enableHovering = false;
        renderer.enabled = false;
    }
}
