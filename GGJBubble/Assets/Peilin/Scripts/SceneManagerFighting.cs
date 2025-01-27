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
    public string even = "Ending Even";     // Scene for healthy condition
    public GameManager gameManager;             // Reference to GameManager
    public Character player1;                   // Reference to player1
    public Character player2;                   // Reference to player2
    private string nextSceneName ;
    

    void update()
    {
        if (gameManager.isGameEnd)
        {
            if (player1.hp > player2.hp)
            {
                nextSceneName = player1Win;
                SceneManager.LoadScene(nextSceneName);
            }
            else if (player1.hp == player2.hp)
            {
                nextSceneName = even;
                SceneManager.LoadScene(nextSceneName);
            }
            if (player1.hp < player2.hp)
            {
                nextSceneName = player2Win;
                SceneManager.LoadScene(nextSceneName);
            }

        }

        if (player1.isDied)
        {

            nextSceneName = player1Win; 
            SceneManager.LoadScene(nextSceneName);
        }

        if (player2.isDied)
        {

            nextSceneName = player2Win;
            SceneManager.LoadScene(nextSceneName);
        }

    }


    
}

