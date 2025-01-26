using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleAnimation : MonoBehaviour
{
    public Sprite[] bubbleImages; // Array of 6 bubble images
    public float animationInterval = 0.5f; // Time interval between sprite changes
    public float initialLoopDuration = 3f; // Duration to loop through all 6 images initially

    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer
    private int currentImageIndex = 0;
    private float timer = 0f;
    private float initialTimer = 0f;
    private bool isInitialLoop = true; // Determines whether we're in the initial loop
    private bool isUsingFirstImage = true; // Determines which image to display in back-and-forth mode

    void Start()
    {
        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Check if we have at least 6 images in the array
        if (bubbleImages.Length < 6)
        {
            Debug.LogError("Please assign at least 6 bubble images in the Inspector!");
            return;
        }

        // Start with the first image
        spriteRenderer.sprite = bubbleImages[0];
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (isInitialLoop)
        {
            // During the initial loop through all 6 images
            initialTimer += Time.deltaTime;

            if (timer >= animationInterval)
            {
                // Move to the next image
                currentImageIndex = (currentImageIndex + 1) % bubbleImages.Length;
                spriteRenderer.sprite = bubbleImages[currentImageIndex];
                timer = 0f;
            }

            // Exit initial loop after the specified duration
            if (initialTimer >= initialLoopDuration)
            {
                isInitialLoop = false;
                currentImageIndex = 0; // Reset for back-and-forth mode
            }
        }
        else
        {
            // Back-and-forth mode between the first and last image
            if (timer >= animationInterval)
            {
                isUsingFirstImage = !isUsingFirstImage;
                spriteRenderer.sprite = isUsingFirstImage ? bubbleImages[bubbleImages.Length-2] : bubbleImages[bubbleImages.Length - 1];
                timer = 0f;
            }
        }
    }
}
