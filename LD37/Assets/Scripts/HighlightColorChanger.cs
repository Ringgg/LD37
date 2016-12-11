using UnityEngine;
using System.Collections;

public class HighlightColorChanger : MonoBehaviour
{
    Hero hero;

    private Renderer highlightRenderer;
    private Color fullHealthColor = Color.green;
    private Color deadColor = Color.red;

    float startSize = 1;
    float endSize = 2;

    float startAlfa = 1;
    float endAlfa = 0;

    float freq = 1.5f;
    float freqTimer;

    Vector3 startScale;

    void Start()
    {
        freqTimer += Random.Range(0.0f, freq);
        startScale = transform.localScale;
        hero = transform.parent.GetComponent<Hero>();
        transform.parent = transform.parent.parent == null? null : transform.parent.parent;
        highlightRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (hero == null)
        {
            Destroy(gameObject);
            return;
        }

        freqTimer = (freqTimer + Time.deltaTime) % freq;
        transform.position = hero.transform.position + Vector3.up * 0.004123f;
        highlightRenderer.material.color = new Color(
            1 - hero.hp / hero.startHP,
            hero.hp / hero.startHP,
            0,
            Mathf.Lerp(startAlfa, endAlfa, freqTimer / freq));

        transform.localScale = startScale * (0.25f + 0.75f * freqTimer / freq);
    }
}
