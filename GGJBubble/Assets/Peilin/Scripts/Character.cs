using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public int hp = 100;
    public TextMeshProUGUI CharacterHP;
    public Slider HPSlider;
    public PKBarController Controller;
    public GameObject garlicPrefab;  // Assign your garlic prefab in the Inspector
    public Transform garlicSpawnPoint;
    // Method to decrease HP
    public void DecreaseHP(int damage)
    {
        // Reduce HP by the damage amount
        hp -= damage;
        Controller.player2Damage += damage;
        Controller.totalDamage += damage;
        Controller.UpdateHealthBar();
        // Log the current HP
        Debug.Log("Character HP: " + hp);
        if (hp < 50 && GameObject.FindWithTag("Garlic") == null)
        {
            SpawnGarlic();
        }
        // Check if HP drops to zero or below
        if (hp <= 0)
        {
            Die();
        }
    }
    void SpawnGarlic()
    {
        GameObject garlic = Instantiate(garlicPrefab, garlicSpawnPoint.position, Quaternion.identity);
        garlic.GetComponent<GarlicToolBehaviour>().character = transform;  // Assign the character to the garlic
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
