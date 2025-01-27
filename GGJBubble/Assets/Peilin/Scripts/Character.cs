using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class Character : MonoBehaviour
{
    public int hp = 100; // 初始HP
    public TextMeshProUGUI CharacterHP; // 角色血量显示
    public Slider HPSlider; // 角色血量条
    public PKBarController controller; // 引用PK控制器
    public int playerID; // 玩家ID (1 或 2)
    public string enemyBubbleTag;
    public bool isGarliced = false;
    public float garlicDuration = 5f;
    private bool isGarlicedCoroutineRunning = false;
    public bool isDied = false;

    public GameObject garlic;

    void Update()
    {
        if (isGarliced)
        {
            garlic.SetActive(true);
        }
        else
        {
            garlic.SetActive(false);
        }
        // 如果 isGarliced 为 true 且协程未运行，启动协程
        if (isGarliced && !isGarlicedCoroutineRunning)
        {
            StartCoroutine(HandleGarlicBuff());
        }

    }

    private IEnumerator HandleGarlicBuff()
    {
        isGarlicedCoroutineRunning = true; // 标记协程正在运行
        Debug.Log("Garlic mode activated. Waiting for 5 seconds...");

        // 等待 5 秒
        yield return new WaitForSeconds(5f);

        // 恢复状态
        isGarliced = false;
        isGarlicedCoroutineRunning = false; // 协程结束
        Debug.Log("Garlic mode deactivated.");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(enemyBubbleTag))
        {
            // 检查碰撞体是否携带伤害值
            SpeechBubble speechBubble = collision.GetComponent<SpeechBubble>();
            if (speechBubble != null)
            {
                // 获取伤害值
                int damage = speechBubble.damage;

                // 调用减血逻辑
                DecreaseHP(damage);
            }
        }
    }

    public void DecreaseHP(int damage)
    {
        // 减少角色自身 HP
        hp -= damage;

        // 更新 UI
        UpdateHPUI();

        // 更新 PKBarController 的进度条和文本
        if (controller != null)
        {
            if (playerID == 1)
                controller.player1HP = hp;
            else if (playerID == 2)
                controller.player2HP = hp;

            controller.UpdateHealthBar();
            controller.UpdateHPText();
        }

        // 检查是否死亡
        if (hp <= 0)
        {
            Die();
        }
    }

    void UpdateHPUI()
    {
        if (CharacterHP != null)
            CharacterHP.text = $"HP: {hp}";

        if (HPSlider != null)
            HPSlider.value = hp;
    }

    void Die()
    {
        Debug.Log($"Player {playerID} has died!");

        isDied = true;

        hp = 0;
       
    }

   
}

