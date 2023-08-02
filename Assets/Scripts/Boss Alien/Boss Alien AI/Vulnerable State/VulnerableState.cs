using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class VulnerableState : Node
{
    private CapsuleCollider2D bossCollider; // Reference to the capsule collider of the boss
    private BoxCollider2D platformCollider; // Reference to the box collider of the platform the boss stands on

    private float counter = 0f; // Counter to track time in the vulnerable state
    private bool collision = true; // Flag to track if the boss should ignore collision with the platform
    private float _waitCounter = 0f; // Counter to track waiting time
    private float _waitTime = 5f; // Time the boss waits in the vulnerable state before resuming normal behavior

    public VulnerableState(CapsuleCollider2D bossCollider, BoxCollider2D platformCollider)
    {
        this.bossCollider = bossCollider; // Initialize the boss capsule collider
        this.platformCollider = platformCollider; // Initialize the platform box collider
    }

    // Override the Evaluate method to define the behavior of the boss's vulnerable state
    public override NodeState Evaluate()
    {
        if (collision == true)
        {
            Physics2D.IgnoreCollision(bossCollider, platformCollider); // Ignore collision between the boss and the platform

            collision = false; // Set the collision flag to false to prevent repeated ignore collisions

            state = NodeState.RUNNING; // The node is still running

            return state;
        }

        else
        {
            Physics2D.IgnoreCollision(bossCollider, platformCollider, false); // Enable collision between the boss and the platform

            collision = true; // Set the collision flag to true for the next vulnerable state

            state = NodeState.RUNNING; // The node is still running

            return state;
        }
    }
}
