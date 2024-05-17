using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float totalTime = 120.0f; // Total time in seconds
    private float currentTime;
    private TMP_Text timerText;
    private bool isGameOver = false; // Flag to prevent multiple game over triggers

    private void Start()
    {
        timerText = GetComponent<TMP_Text>();
        currentTime = totalTime;
    }

    private void Update()
    {
        if (!isGameOver)
        {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                UpdateTimerDisplay();
            }
            else
            {
                currentTime = 0; // Ensure it doesn't go negative
                UpdateTimerDisplay(); // Update timer to display 00:00
                ShowGameOverScreen();
                isGameOver = true; // Prevent multiple triggers
            }
        }
    }

    private void UpdateTimerDisplay()
    {
        // If time is greater than 0, continue updating timer normally
        if (currentTime > 0)
        {
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);
            string formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);
            timerText.text = formattedTime;
        }
        else
        {
            // If time is 0 or less, display 00:00
            timerText.text = "00:00";
        }
    }

    private void ShowGameOverScreen()
    {
        Debug.Log("Game Over!");
        // Implement logic to display the Game Over UI (e.g., enable canvas, set text, etc.)
        // You can also handle other actions like restarting the level or going back to the main menu.
        // Example: SceneManager.LoadScene("GameOverScene");
    }
}