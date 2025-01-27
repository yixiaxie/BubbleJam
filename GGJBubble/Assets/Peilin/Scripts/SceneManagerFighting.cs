using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerFighting : MonoBehaviour
{
    // 公有字段
    public string player1Win = "Ending";    // 玩家1胜利场景
    public string player2Win = "Ending 2"; // 玩家2胜利场景
    public string even = "Ending Even";    // 平局场景
    public GameManager gameManager;        // GameManager引用
    public Character player1;              // 玩家1引用
    public Character player2;              // 玩家2引用

    void Update()
    {
        // 检查是否有引用为空
        if (gameManager == null || player1 == null || player2 == null)
        {
            Debug.LogError("SceneManagerFighting: 缺少必要的引用！请确保 GameManager、Player1 和 Player2 已赋值。");
            return;
        }

        // 判断游戏是否结束
        if (gameManager.isGameEnd)
        {
            HandleGameEnd();
        }

        // 判断玩家是否死亡
        if (player1.isDied)
        {
            SceneManager.LoadScene(player2Win); // 玩家2胜利
        }
        else if (player2.isDied)
        {
            SceneManager.LoadScene(player1Win); // 玩家1胜利
        }
    }

    private void HandleGameEnd()
    {
        // 判断玩家血量
        if (player1.hp > player2.hp)
        {
            SceneManager.LoadScene(player1Win); // 玩家1胜利
        }
        else if (player1.hp < player2.hp)
        {
            SceneManager.LoadScene(player2Win); // 玩家2胜利
        }
        else
        {
            SceneManager.LoadScene(even); // 平局
        }
    }
}
