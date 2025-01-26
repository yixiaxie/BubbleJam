using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudienceAnimation : MonoBehaviour
{
    public Sprite sprite1; // Assign the first sprite in the Inspector
    public Sprite sprite2; // Assign the second sprite in the Inspector
    public float switchInterval = 2f; // Time interval in seconds to switch sprites

    private Image uiImage; // For UI Image component
    private SpriteRenderer spriteRenderer; // For SpriteRenderer component
    private bool isUsingFirstSprite = true;
    private float timer;

    void Start()
    {
        // Try to get either SpriteRenderer or Image (depending on the object)
        spriteRenderer = GetComponent<SpriteRenderer>();
        uiImage = GetComponent<Image>();

        // Set the initial sprite
        SetSprite(sprite1);
    }

    void Update()
    {
        // Update timer
        timer += Time.deltaTime;

        // Switch sprite when the timer exceeds the interval
        if (timer >= switchInterval)
        {
            isUsingFirstSprite = !isUsingFirstSprite;
            SetSprite(isUsingFirstSprite ? sprite1 : sprite2);
            timer = 0f;
        }
    }

    void SetSprite(Sprite sprite)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = sprite;
        }
        else if (uiImage != null)
        {
            uiImage.sprite = sprite;
        }
    }
}
