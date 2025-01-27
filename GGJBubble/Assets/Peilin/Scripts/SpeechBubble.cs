using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class SpeechBubble : MonoBehaviour
{
    public int damage = 10; // Damage dealt by the speech bubble
    public float speed = 10f; // Speed at which the speech bubble flies
    public KeyCode playerKey = KeyCode.Return;
    public int playerID; // 玩家ID (1 或 2)
    public string playerName = "Character";
    public string targetPlayerName = "Character";
    private Transform target; // Reference to the character's transform
   public bool isFlying = false; // Flag to check if the bubble is flying
    public float angleOffsetRange = 60f;
    public int garlicDamage = 2;
    

    void Update()
    {
        // Check for the Enter key press
        if (Input.GetKeyDown(playerKey))
        {
            
                // 找到带有特定标签的物体
                GameObject player = GameObject.FindWithTag(playerName);

                if (player != null)
                {
                    // 获取 SpriteRenderer 组件
                    SpriteController spriteController = player.GetComponent<SpriteController>();

                spriteController.isAttacking = true;

                }


                // Find the character in the scene
                GameObject character = GameObject.FindWithTag(targetPlayerName);


            if (character != null)
            {
                target = character.transform; // Set the target
                isFlying = true; // Set the flying flag
            }
            else
            {
                Debug.LogWarning("No character found in the scene!");
            }
        }

        // If the bubble is flying, move toward the character
        if (isFlying && target != null)
        {
            // 计算气泡移动的基本方向
            Vector2 direction = (Vector2)target.position - (Vector2)transform.position;

            // 添加随机角度偏移
            float randomAngleOffset = Random.Range(-angleOffsetRange, angleOffsetRange);
            float originalAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            float finalAngle = originalAngle + randomAngleOffset;

            // 将角度转为方向向量
            Vector2 randomDirection = new Vector2(Mathf.Cos(finalAngle * Mathf.Deg2Rad), Mathf.Sin(finalAngle * Mathf.Deg2Rad));

            // 计算上下起伏偏移（正弦波）
            float waveOffset = Mathf.Sin(Time.time * speed) * 0.03f; // 振幅为0.5，可调整
            Vector3 waveMotion = new Vector3(0, waveOffset, 0);

            // 最终方向叠加上下波动
            transform.position += (Vector3)(randomDirection.normalized * speed * Time.deltaTime) + waveMotion;
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the speech bubble hits the character
        Character character = collision.GetComponent<Character>();
        SpriteController spriteController = collision.GetComponent<SpriteController>();

        
        if (collision.CompareTag(targetPlayerName))
        {
           
            

            spriteController.isHurt = true;
            Debug.Log("HIT");

            int finalDamage = damage; // 默认伤害

            if (character.isGarliced)
            { 
                finalDamage*= garlicDamage;
            }

            // 扣除目标玩家 HP
            character.DecreaseHP(finalDamage);

            // 销毁当前气泡
            Destroy(gameObject);
        }

    }
}
