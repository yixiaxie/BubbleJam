using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicAnimation : MonoBehaviour
{
    public Sprite[] garlicImages; // Array of 4 garlic images
    public float animationInterval = 0.2f; // Time interval between sprite changes

    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer
    private int currentImageIndex = 0; // Tracks the current image index
    private float timer = 0f; // Timer to control animation

    void Start()
    {
        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Check if we have exactly 4 images
        if (garlicImages.Length != 4)
        {
            Debug.LogError("Please assign exactly 4 garlic images in the Inspector!");
            return;
        }

        // Set the initial sprite to the first image
        spriteRenderer.sprite = garlicImages[0];
    }

    void Update()
    {
        // Increment the timer
        timer += Time.deltaTime;

        // Switch to the next image if the timer exceeds the animation interval
        if (timer >= animationInterval)
        {
            // Update the current image index to loop back and forth
            currentImageIndex = (currentImageIndex + 1) % garlicImages.Length;

            // Set the sprite to the new image
            spriteRenderer.sprite = garlicImages[currentImageIndex];

            // Reset the timer
            timer = 0f;
        }
    }
}
