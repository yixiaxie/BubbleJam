using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlScene : MonoBehaviour
{
    public string nextSceneName = "PolishedVersion"; // 下一个场景的名称

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
