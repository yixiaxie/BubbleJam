using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningScene : MonoBehaviour
{
    public string nextSceneName = "Credit 2"; 

    void Update()
    {

        if (Input.anyKeyDown)
        {

            SceneManager.LoadScene(nextSceneName);
        }
    }
}
