using UnityEngine;
using System.Collections;

public class HighlightColorChanger : MonoBehaviour
{
    private Renderer highlightRenderer;
    private Color fullHealthColor = Color.green;
    private Color deadColor = Color.red;

    void Start()
    {
        highlightRenderer = GetComponent<Renderer>();
    }

    public void ChangeHighlightColor(float currentHP, float startHP)
    {
        highlightRenderer.material.color = Color.Lerp(deadColor, fullHealthColor, currentHP / startHP);
    }
}
