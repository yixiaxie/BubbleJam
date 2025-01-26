using System.Collections;
using UnityEngine;
using TMPro;

public class CountDown : MonoBehaviour
{
    public TextMeshProUGUI countdownText; // Assign in Inspector
    public GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance; // Access the GameManager
        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        countdownText.gameObject.SetActive(true); // Show the countdown text
        gameManager.isGameActive = false;
        // Countdown from 3 to 1
        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString(); // Update text with countdown number
            yield return new WaitForSeconds(1f); // Wait for 1 second
        }
        // Start the game by activating the timer
        gameManager.isGameActive = true;
        countdownText.text = "GO!"; // Display "GO!" before starting the game
        yield return new WaitForSeconds(1f); // Wait for 1 second

        countdownText.gameObject.SetActive(false); // Hide the countdown text

        
    }
}
