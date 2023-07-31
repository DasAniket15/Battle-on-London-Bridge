using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using Utils;

public class VulnerableStateTrigger : Node
{
    private BossHealth bossHealth; // Reference to the BossHealth script attached to the boss
    private ProjectileController projectileController; // Reference to the ProjectileController script
    public bool vulnerability; // Flag indicating if the boss is in the vulnerable state
    private static float _waitCounter = 0f; // Counter to track wait times
    private int currentHealth; // Current health of the boss
    public int collisionThreshold = 5; // Threshold for triggering vulnerability
    public bool vulnerabilityOver = false; // Flag indicating if the vulnerability is over
    private int bulletHit; // The number of times the boss needs to be hit by projectiles to trigger vulnerability
    private int waitTime; // The duration of the vulnerable state (time until vulnerability ends)
    private int damageToBoss; // Damage dealt to the boss during the vulnerable state

    public VulnerableStateTrigger(BossHealth bossHealth, ProjectileController projectileController, int bulletHit, int waitTime, int damageToBoss)
    {
        this.bossHealth = bossHealth; // Initialize the BossHealth reference
        this.projectileController = projectileController; // Initialize the ProjectileController reference
        this.bulletHit = bulletHit; // Initialize the bullet hit count needed to trigger vulnerability
        this.waitTime = waitTime; // Initialize the vulnerable state duration
        this.damageToBoss = damageToBoss; // Initialize the damage dealt to the boss during vulnerability
    }

    // Override the Evaluate method to define the behavior of the boss's vulnerable state trigger
    public override NodeState Evaluate()
    {
        currentHealth = bossHealth.GetCurrentHealth(); // Get the current health of the boss

        Debug.Log(currentHealth);

        if (projectileController.GetCounter() == bulletHit)
        {
            // If the boss has been hit the required number of times, trigger vulnerability.
            vulnerability = true;

            projectileController.SetCounter(bulletHit + 1); // Increase the bullet hit count to prevent repeated triggers
            projectileController.SetDamage(damageToBoss); // Set the damage dealt to the boss during vulnerability

            // Use FunctionTimer to schedule the vulnerableAction method to be called after the wait time.
            FunctionTimer.Create(vulnerableAction, waitTime);

            state = NodeState.SUCCESS; // The vulnerable state trigger succeeds

            return state;
        }
        else if (vulnerabilityOver == true)
        {
            // If the vulnerability is over (vulnerableAction method has been called), reset the vulnerability state.
            vulnerabilityOver = false;
            vulnerability = false;

            projectileController.SetCounter(0); // Reset the bullet hit count
            projectileController.SetDamage(0); // Reset the damage dealt to the boss during vulnerability

            state = NodeState.RUNNING; // The vulnerable state trigger is still running

            return state;
        }
        else
        {
            // The vulnerable state trigger fails if the conditions are not met.
            state = NodeState.FAILURE;

            return state;
        }
    }

    // Method called when the vulnerability is over.
    private void vulnerableAction()
    {
        vulnerabilityOver = true; // Set the vulnerability over flag to true

        _waitCounter = _waitCounter + 1; // Increment the wait counter to track the number of times the vulnerability is triggered.

        Debug.Log("wait times" + _waitCounter);
    }
}
