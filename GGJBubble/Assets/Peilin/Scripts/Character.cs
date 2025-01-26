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
    public GarlicToolBehaviour garlic;  
    public Transform garlicSpawnPoint;
    public Sprite garlicConsumedSprite;
    public SpriteRenderer characterSpriteRenderer;
    public Sprite OriginalSprite;
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
        if (hp < 50)
        {
            Debug.Log("garlic is here!");
            garlic.MoveToCharacter();
        }
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
    public void ChangeCharacterSprite()
    {
        Debug.Log("Changing to stinky face");
        StartCoroutine(ChangeSpriteTemporarily());
    }

    IEnumerator ChangeSpriteTemporarily()
    {
        Debug.Log("stinky face");
        if (characterSpriteRenderer != null && garlicConsumedSprite != null)
        {
            // Change to garlic consumed sprite
            characterSpriteRenderer.sprite = garlicConsumedSprite;

            // Wait for 5 seconds
            yield return new WaitForSeconds(5f);

            // Revert back to the original sprite
            characterSpriteRenderer.sprite = OriginalSprite;
        }
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
