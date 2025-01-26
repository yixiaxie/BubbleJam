using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PKBarController : MonoBehaviour
{
    [SerializeField] private Image healthBarFill;  // Reference to the fill image
    [SerializeField] private Color fullHealthColor = Color.green;  // Color for full health
    [SerializeField] private Color lowHealthColor = Color.red;    // Color for low health
    public float totalDamage=100f;
    public float player1Damage = 50f;
    public float player2Damage = 50f;
    /// <summary>
    /// Updates the health bar fill amount and color.
    /// </summary>
    /// <param name="healthPercentage">Current health percentage (0 to 1).</param>
    public void UpdateHealthBar()
    {
        float healthPercentage=player1Damage/totalDamage;
        // Clamp the health percentage to ensure it's between 0 and 1
        healthPercentage = Mathf.Clamp01(healthPercentage);

        // Update the fill amount
        healthBarFill.fillAmount = healthPercentage;

    }
}
