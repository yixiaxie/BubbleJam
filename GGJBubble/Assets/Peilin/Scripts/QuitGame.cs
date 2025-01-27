using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public float quitDelay = 6f; // Delay in seconds before quitting the game

    void Start()
    {
        // Invoke the QuitGame method after the specified delay
        Invoke("Quit", quitDelay);
    }

    void Quit()
    {
#if UNITY_EDITOR
        // If running in the Unity Editor, stop play mode
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // If running as a build, quit the application
            Application.Quit();
#endif

        Debug.Log("Game has quit."); // Optional log for debugging
    }
}
