using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerFighting : MonoBehaviour
{
    // Public fields
    public string lowHealthScene = "Ending";  // Scene for low health
    public string gameOverScene = "Ending 2";   // Scene for game over
    public string healthyScene = "Ending Even";     // Scene for healthy condition
    public GameManager gameManager;             // Reference to GameManager
    public Character player1;                   // Reference to player1
    public Character player2;                   // Reference to player2

    void Update()
    {
        // Ensure the player1 object is assigned
        if (player1 == null)
        {
            Debug.LogError("Player1 is not assigned to SceneManagerFighting!");
            return;
        }

        HandleSceneTransition();
    }

    private void HandleSceneTransition()
    {
        if (player1.hp <= 0)
        {
            // Player1 has no health left, load GameOver scene
            LoadScene(gameOverScene);
        }
        else if (player1.hp > 0 && player1.hp <= 30)
        {
            // Player1 is in low health range, load LowHealth scene
            LoadScene(lowHealthScene);
        }
        else if (player1.hp > 30)
        {
            // Player1 is healthy, load Healthy scene
            LoadScene(healthyScene);
        }
    }

    private void LoadScene(string sceneName)
    {
        // Only load the scene if it’s not already active
        if (SceneManager.GetActiveScene().name != sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}

