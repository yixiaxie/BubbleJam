using UnityEngine;
using TMPro;  // Add the namespace for TextMeshPro

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isGameActive = false;
    private float countdownTime = 60f;  // Countdown timer

    public TextMeshProUGUI timerText;  // Reference to the TextMeshProUGUI element

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Ensure the timer text is initially set up
        if (timerText != null)
        {
            timerText.text = "Time: " + countdownTime.ToString("F0") + "s";  // Display the starting time
        }
    }

    void Update()
    {
        if (isGameActive && countdownTime > 0)
        {
            countdownTime -= Time.deltaTime;  // Decrease time

            // Update the timer text on screen
            if (timerText != null)
            {
                timerText.text = "Time: " + Mathf.Ceil(countdownTime).ToString("F0") + "s";  // Round and display time
            }
        }
        else if (countdownTime <= 0 && isGameActive)
        {
            countdownTime = 0;
            EndGame();  // End game when time runs out
        }
    }

    public void EndGame()
    {
        isGameActive = false;  // Stop the game
        if (timerText != null)
        {
            timerText.text = "Time's up!";  // Display message when time is up
        }

    }

    public float GetTimeRemaining()
    {
        return countdownTime;
    }

    // Method to start the game after the countdown
    public void StartGame()
    {
        isGameActive = true;  // Activate the timer
    }

    // Method to reset the game
    public void ResetGame()
    {
        countdownTime = 60f;
        isGameActive = false;
        if (timerText != null)
        {
            timerText.text = "Time: " + countdownTime.ToString("F0") + "s";  // Reset the text
        }
    }
}
