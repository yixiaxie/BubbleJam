using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerFighting : MonoBehaviour
{
    // Public fields
    public string player1Win = "Ending";  // Scene for low health
    public string player2Win = "Ending 2";   // Scene for game over
    public string evenScene = "Ending Even";     // Scene for healthy condition
    public GameManager gameManager;             // Reference to GameManager
    public Character player1;                   // Reference to player1
    public Character player2;                   // Reference to player2
    private string nextScene;

    void Update()
    {
        if (gameManager.isGameEnd)
        {
            if (player1.hp > player2.hp)
            {
                nextScene = player1Win;
                SceneManager.LoadScene(nextScene);
            }
            else if (player1.hp == player2.hp)
            {
                nextScene = evenScene;
                SceneManager.LoadScene(nextScene);
            }
            else if (player1.hp < player2.hp)
            {
                nextScene = player2Win;
                SceneManager.LoadScene(nextScene);
            }
        }

        
            if (player1.isDied)
            {
                nextScene = player2Win;
                SceneManager.LoadScene(nextScene);
            }

            if (player2.isDied)
            {
                nextScene = player1Win;
                SceneManager.LoadScene(nextScene);
            }
           
        }

        
    }

    


