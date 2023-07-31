using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 500;
    private int currentHealth;


    private void Start()
    {
        currentHealth = maxHealth;
    }


    public void TakeDamage(int damageAmount)
    {
        Debug.Log("Boss hit!");

        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Defeated();
        }
    }


    private void Defeated()
    {
        Debug.Log("Boss defeated!");

        Destroy(gameObject);
    }


    public int GetCurrentHealth()
    { 
        return currentHealth;
    }


    public void SetCurrentHealth(int currentHealth)
    {
        this.currentHealth = currentHealth;
    }
}
