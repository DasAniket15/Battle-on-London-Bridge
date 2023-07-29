using UnityEngine;


public class BossHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;


    private void Start()
    {
        currentHealth = maxHealth;
    }


    public void TakeDamage(int damageAmount)
    {
        Debug.Log("Boss hit!");
        currentHealth -= damageAmount;

        // Check if the boss is defeated
        if (currentHealth <= 0)
        {
            Defeated();
        }
    }


    private void Defeated()
    {
        // Handle boss defeat logic here (e.g., play defeat animation, destroy the boss, etc.)
        Debug.Log("Boss defeated!");
        Destroy(gameObject);
    }
}
