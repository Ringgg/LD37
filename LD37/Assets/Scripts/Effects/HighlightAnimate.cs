using UnityEngine;
using System.Collections;

public class HighlightAnimate : MonoBehaviour
{
    private Renderer highlightRenderer;
    private Color fullHealthColor = Color.green;
    private Color deadColor = Color.red;

    public float startSize = 1;
    public float endSize = 1.0f;

    public float startAlfa = 0.6f;
    public float endAlfa = 0;

    public float freq = 1.0f;
    public float vel = 0;
    float freqTimer;

    Vector3 startScale;
    Vector3 startPos;

    void Start()
    {
        startScale = transform.localScale;
        highlightRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        freqTimer = (freqTimer + Time.deltaTime);

        if (freqTimer >= freq)
        {
            Destroy(gameObject);
            return;
        }

        transform.position += Vector3.up * Time.deltaTime * vel;
        float progress = freqTimer / freq;
        //transform.position += Vector3.up * 5 * Time.deltaTime;
        highlightRenderer.material.color = new Color(
            highlightRenderer.material.color.r,
            highlightRenderer.material.color.g,
            highlightRenderer.material.color.b,
            Mathf.Lerp(startAlfa, endAlfa, 4 * (progress - progress * progress)));
        transform.localScale = startScale * Mathf.Lerp(startSize, endSize, progress);
    }
}
