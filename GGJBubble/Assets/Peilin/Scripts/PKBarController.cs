using UnityEngine;
using TMPro;

public class PKBarController : MonoBehaviour
{
    [Header("Player HP")]
    public int player1HP = 100; // 玩家1初始HP
    public int player2HP = 100; // 玩家2初始HP
    public int maxHP = 100;     // 最大HP
    public GarlicToolBehaviour garlic1;
    public GarlicToolBehaviour garlic2;

    [Header("UI Elements")]
    public RectTransform player1Bar;      // 玩家1的进度条部分
    public RectTransform player2Bar;      // 玩家2的进度条部分
    public RectTransform barParent;       // 总进度条的父级
    public TextMeshProUGUI player1HPText; // 玩家1的HP文本
    public TextMeshProUGUI player2HPText; // 玩家2的HP文本
    public float player1Ratio;
    public float player2Ratio;
    [Header("Game Settings")]
    public bool gameOver = false;
    void Start()
    {
        UpdateHealthBar();
        UpdateHPText();
    }

    /// <summary>
    /// 对某玩家造成伤害并更新状态
    /// </summary>
    /// <param name="playerID">1 为玩家1，2 为玩家2</param>
    /// <param name="damage">造成的伤害值</param>
    public void DealDamage(int playerID, int damage)
    {
        CheckGameOver();
        if (gameOver) return;

        if (playerID == 1)
        {
            player1HP = Mathf.Max(0, player1HP - damage);
        }
        else if (playerID == 2)
        {
            player2HP = Mathf.Max(0, player2HP - damage);
        }

        UpdateHealthBar();
        UpdateHPText();
        
    }

    public void UpdateHealthBar()
    {
        int totalHP = Mathf.Max(1, player1HP + player2HP);

        player1Ratio = (float)player1HP / totalHP;
        player2Ratio = (float)player2HP / totalHP;
        Debug.Log("player1 ratio is: "+player1Ratio);
        Debug.Log("player2 ratio is: " + player2Ratio);
        float barWidth = barParent.rect.width;
        player1Bar.sizeDelta = new Vector2(barWidth * player1Ratio, player1Bar.sizeDelta.y);
        player2Bar.sizeDelta = new Vector2(barWidth * player2Ratio, player2Bar.sizeDelta.y);
    }

    public void UpdateHPText()
    {
        if (player1HPText != null)
            player1HPText.text = $"HP: {player1HP}";

        if (player2HPText != null)
            player2HPText.text = $"HP: {player2HP}";
    }

    void CheckGameOver()
    {
        if (player1HP <= 0 && player2HP <= 0)
        {
            Debug.Log("It's a draw!");
            gameOver = true;
        }
        else if (player1HP <= 0)
        {
            Debug.Log("Player 2 Wins!");
            gameOver = true;
        }
        else if (player2HP <= 0)
        {
            Debug.Log("Player 1 Wins!");
            gameOver = true;
        }
    }
}
