using UnityEngine;

public class PlayerHealthUI : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject heartPrefab;
    public Transform heartContainer;

    private void Start()
    {
        SpawnHearts();
    }

    public void SpawnHearts()
    {
        // Clear any existing hearts
        foreach (Transform child in heartContainer)
        {
            Destroy(child.gameObject);
        }

        float heartWidth = 1.5f; // Width of the heart sprite, adjust as needed
        float spacing = 0.00000005f; // Spacing between hearts, adjust as needed

        Vector3 spawnPosition = heartContainer.position;
        spawnPosition.x -= ((playerHealth.maxHealth - 1) * (heartWidth + spacing)) / 2f;

        // Spawn hearts based on player's maximum health
        for (int i = 0; i < playerHealth.maxHealth; i++)
        {
            Instantiate(heartPrefab, spawnPosition, Quaternion.identity, heartContainer);
            spawnPosition.x += heartWidth + spacing;
        }
    }

    public void UpdateHearts()
    {
        int currentHealth = Mathf.RoundToInt(playerHealth.GetCurrentHealth());

        // Deactivate hearts based on player's current health
        for (int i = 0; i < heartContainer.childCount; i++)
        {
            Transform heart = heartContainer.GetChild(i);
            heart.gameObject.SetActive(i < currentHealth);
        }
    }
}
