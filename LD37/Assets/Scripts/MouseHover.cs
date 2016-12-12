using UnityEngine;

public class MouseHover : MonoBehaviour
{
    public GameObject trasformablePlane;

    private Vector3 startPos;
    private Renderer renderer;
    private bool enableHovering;
    public LayerMask layerMask;

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
        
        if (IsMousePointing() == false) return;
        renderer.enabled = true;
        freqTimer = (freqTimer + Time.deltaTime) % freq;
        trasformablePlane.transform.position = trasformablePlane.transform.position + Vector3.up * 0.004123f;
        trasformablePlane.transform.localScale = startScale * (0.25f + 0.75f * freqTimer / freq);
    }

    private bool IsMousePointing()
    {
        RaycastHit hitInfo;
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(r, out hitInfo, 1000, layerMask))
        {
            renderer.enabled = false;
            return false;
        }
        if (hitInfo.collider.gameObject == gameObject)
        {
            return true;
        }
        renderer.enabled = false;
        return false;
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
