using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Character2 : MonoBehaviour
{
    public int hp = 100;
    public TextMeshProUGUI CharacterHP;
    public Slider HPSlider;
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

    public void Update()
    {
        UpdateHPUI();
    }
    // Method called when the character's HP reaches 0
    private void Die()
    {
        Debug.Log("Character has died!");
        // Add your logic for when the character dies (e.g., destroy object, restart level, etc.)
        Destroy(gameObject);
    }
    private void UpdateHPUI()
    {
        // Update Text
        if (CharacterHP != null)
            CharacterHP.text = "HP: " + hp;
        else
        {
            Debug.LogWarning("HP Text is not assigned!");
        }

        // Update Slider
        if (HPSlider != null)
            HPSlider.value = hp;
        else
        {
            Debug.LogWarning("HP slider is not assigned!");
        }
    }
}
