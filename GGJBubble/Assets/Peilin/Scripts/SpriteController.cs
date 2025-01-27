using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    public Sprite idleSprite;    // 闲置状态图片
    public Sprite attackSprite;  // 攻击状态图片
    public Sprite hurtSprite;    // 受伤状态图片

    public float displayDuration = 0.5f; // 每种状态显示的时间（秒）

    private float attackTimer = 0f; // 攻击状态的计时器
    private float hurtTimer = 0f;   // 受伤状态的计时器
    public bool isAttacking;
    public bool isHurt;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 如果正在攻击
        if (isAttacking)
        {
            spriteRenderer.sprite = attackSprite;

            // 开始计时
            attackTimer += Time.deltaTime;
            if (attackTimer >= displayDuration)
            {
                isAttacking = false; // 显示时间结束后，重置状态
                attackTimer = 0f;   // 重置计时器
            }
        }
        else if (isHurt)
        {
            spriteRenderer.sprite = hurtSprite;

            // 开始计时
            hurtTimer += Time.deltaTime;
            if (hurtTimer >= displayDuration)
            {
                isHurt = false; // 显示时间结束后，重置状态
                hurtTimer = 0f; // 重置计时器
            }
        }
        else
        {
            // 如果没有其他状态，显示 idle 图片
            spriteRenderer.sprite = idleSprite;
        }
    }
}
