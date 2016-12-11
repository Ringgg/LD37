using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainUI : MonoBehaviour
{
    public Button startButton;
    public Image bossHP;

    public void Awake()
    {
        Time.timeScale = 0.0f;
    }

    public void StartPressed()
    {
        startButton.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void OnGUI()
    {
        bossHP.fillAmount = (float) Boss.instance.hp / Boss.instance.maxHp;
    }
}
