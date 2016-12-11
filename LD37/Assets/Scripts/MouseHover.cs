using UnityEngine;
using System.Collections;

public class MouseHover : MonoBehaviour
{
    private Color startcolor;
    private readonly Color hovercolor = Color.black;
    private Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        startcolor = renderer.material.color;
    }
    void OnMouseEnter()
    {
        renderer.material.color = hovercolor;
    }
    void OnMouseExit()
    {
        ReturnToStartColor();
    }

    public void ReturnToStartColor()
    {
        renderer.material.color = startcolor;
    }
}
