using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    private float currentHealth;

    public PlayerDefeat playerDefeat; // Reference to the GameOverPlayerDefeat script

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log("Player health: " + currentHealth);

        if (currentHealth <= 0)
        {
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
