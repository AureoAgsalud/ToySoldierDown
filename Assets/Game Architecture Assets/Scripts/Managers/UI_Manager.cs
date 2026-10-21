using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Playables;

public class UI_Manager : MonoBehaviour
{
    public TMP_Text healthDisplay;
    public GameObject gameOverPanel;
    public PlayableDirector gameOverTimeline;
    
    public HealthScript healthScript;
    private void Awake()
    {
        healthScript.OnHealthChanged += UpdateHealthDisplay;

        healthScript.OnDeath += ShowGameOver;
    }

    // - Update health text
    public void UpdateHealthDisplay(float currentHealth)
    {
        healthDisplay.text = "Health: " + currentHealth.ToString("F0");
    }
     // - Show a GameOver Screen
    public void ShowGameOver()
    {
        gameOverTimeline.Play();
        //gameOverPanel.SetActive(true);
    }
}
