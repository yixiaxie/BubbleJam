using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class SpeechBubble2 : MonoBehaviour
{
    public int damage = 10; // Damage dealt by the speech bubble
    public float speed = 10f; // Speed at which the speech bubble flies
    public KeyCode player2Key = KeyCode.Space;
    private Transform target; // Reference to the character's transform
    private bool isFlying = false; // Flag to check if the bubble is flying

    void Update()
    {
        // Check for the Enter key press
        if (Input.GetKeyDown(player2Key))
        {
            // Find the character in the scene
            GameObject character = GameObject.FindWithTag("Character2");

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
            // Move the speech bubble toward the target
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            // Optionally rotate the bubble to face the target (optional)
            Vector2 direction = target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the speech bubble hits the character
        Character2 character = collision.GetComponent<Character2>();

        if (character != null)
        {
            // Decrease the character's HP
            character.DecreaseHP(damage);

            // Destroy the speech bubble
            Destroy(gameObject);
        }
    }
}
