using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 500; // Maximum health of the boss
    private int currentHealth; // Current health of the boss

    private void Start()
    {
        currentHealth = maxHealth; // Set the current health to the maximum health when the boss starts
    }

    // Method called when the boss takes damage
    public void TakeDamage(int damageAmount)
    {
        Debug.Log("Boss hit!");

        currentHealth -= damageAmount; // Reduce the current health by the damage amount received

        // Check if the boss's health has reached zero or below
        if (currentHealth <= 0)
        {
            Defeated(); // Call the Defeated method to handle boss defeat
        }
    }

    // Method called when the boss is defeated
    private void Defeated()
    {
        Debug.Log("Boss defeated!");

        Destroy(gameObject); // Destroy the boss GameObject
    }

    // Method to get the current health of the boss
    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    // Method to set the current health of the boss
    public void SetCurrentHealth(int currentHealth)
    {
        this.currentHealth = currentHealth;
    }
}
