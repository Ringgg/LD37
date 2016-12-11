using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainUI : MonoBehaviour
{
    public Button startButton;
    public Image bossHP;
    public Text victoryPrompt;
    public GameObject endScreen;

    public void Awake()
    {
        Time.timeScale = 0.0f;
        startButton.gameObject.SetActive(true);
        EventManager.StartListening(EventType.BossDied, GameOver);
        EventManager.StartListening(EventType.HeroesKilled, GameOver);
    }

    public void StartPressed()
    {
        startButton.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void GameOver(object o)
    {
        endScreen.SetActive(true);
        victoryPrompt.text = o != null ? "You were defeated!" : "You wiped the party! Yey!";
    }

    public void OnGUI()
    {
        bossHP.fillAmount = (float) Boss.instance.hp / Boss.instance.maxHp;
    }
}
