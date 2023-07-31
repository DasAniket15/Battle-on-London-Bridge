using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCollisionCounter : MonoBehaviour
{
    // Singleton pattern instance
    public static BossCollisionCounter Instance { get; private set; }

    // The current collision count with the boss
    public int BossCollisionCount { get; private set; }

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Implement Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            // If an instance already exists, destroy this instance to ensure only one exists
            Destroy(gameObject);
        }
    }

    // Method to increase the collision count with the boss
    public void IncreaseCollisionCount()
    {
        BossCollisionCount++;
    }
}
