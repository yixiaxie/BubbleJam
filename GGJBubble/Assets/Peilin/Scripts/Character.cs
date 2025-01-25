using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int hp = 100;

    // Method to decrease HP
    public void DecreaseHP(int damage)
    {
        // Reduce HP by the damage amount
        hp -= damage;

        // Log the current HP
        Debug.Log("Character HP: " + hp);

        // Check if HP drops to zero or below
        if (hp <= 0)
        {
            Die();
        }
    }

    // Method called when the character's HP reaches 0
    private void Die()
    {
        Debug.Log("Character has died!");
        // Add your logic for when the character dies (e.g., destroy object, restart level, etc.)
        Destroy(gameObject);
    }
}
