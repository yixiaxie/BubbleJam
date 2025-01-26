using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameEndingManager : MonoBehaviour
{
    public TextMeshProUGUI winnerText; // Assign a UI Text element in the Inspector
    public Button restartButton; // Assign a UI Button element in the Inspector
    public Button exitButton; // Assign a UI Button element in the Inspector

    private int player1Score; // Replace with your game logic for Player 1's score
    private int player2Score; // Replace with your game logic for Player 2's score

    void Start()
    {
        // Simulated game result for testing (Replace with actual game logic)
        
        DisplayWinner();

        // Add button listeners
        restartButton.onClick.AddListener(RestartGame);
        exitButton.onClick.AddListener(ExitGame);
    }

    void DisplayWinner()
    {
        if (player1Score > player2Score)
        {
            winnerText.text = "Player 1 Wins!";
        }
        else if (player2Score > player1Score)
        {
            winnerText.text = "Player 2 Wins!";
        }
        else
        {
            winnerText.text = "It's a Tie!"; // Optional if scores are equal
        }
    }

    void RestartGame()
    {
        SceneManager.LoadScene("Open"); // Reload the current scene
    }

    void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game Exited"); // For testing in the Unity Editor
    }
}
