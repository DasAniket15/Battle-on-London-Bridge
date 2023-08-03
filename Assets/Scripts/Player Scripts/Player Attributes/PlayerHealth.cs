using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    private float currentHealth;

    // TODO: Add any visual feedback for player taking damage (e.g., flashing)

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log("Player health: " + currentHealth);

        // TODO: Handle any effects for taking damage, e.g., screen shake, sound, etc.

        if (currentHealth <= 0)
        {
            // TODO: Implement game over or player death logic
            Debug.Log("Player has no more hearts. Game Over!");
        }

        // Update the hearts UI
        PlayerHealthUI playerHealthUI = FindObjectOfType<PlayerHealthUI>();
        if (playerHealthUI != null)
        {
            playerHealthUI.UpdateHearts();
        }
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}
