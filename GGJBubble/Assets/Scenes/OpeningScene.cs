using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningScene2 : MonoBehaviour
{
    public string nextSceneName = "Control"; // 下一个场景的名称
    void Start()
    {
        Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);
    }

    void Update()
    {
        // 检测玩家按下任意键
        if (Input.anyKeyDown)
        {
            // 切换到下一个场景
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
