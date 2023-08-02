using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class BossBT : BehaviorTree.Tree
{
    public UnityEngine.Transform[] towers; // Array of tower transforms the boss will hop between
    public static float speed = 10f; // Speed of the boss
    public Rigidbody2D rb; // Rigidbody2D component of the boss

    [SerializeField] private CircleCollider2D bulletCollider; // Circle collider for the boss's bullets
    [SerializeField] private CapsuleCollider2D bossCollider; // Capsule collider for the boss
    [SerializeField] private BoxCollider2D platformCollider; // Box collider for the platform the boss stands on

    [SerializeField] private int bulletHit; // Number of hits the boss can take from bullets
    [SerializeField] private int waitTime; // Time the boss waits in vulnerable state after getting hit
    [SerializeField] private int damageToBoss; // Damage dealt by boss to other objects
    [SerializeField] private int vulnerableBulletHit;

    public GameObject prefab; // Prefab of the projectile the boss fires

    private static BossHealth bossHealth; // Reference to the boss health component
    private static ProjectileController projectileController; // Reference to the projectile controller component

    private VulnerableStateTrigger vulnerableStateTrigger; // Custom node to trigger boss's vulnerable state

    // Override the SetupTree method to create the behavior tree
    protected override Node SetupTree()
    {
        // Get references to the boss health and projectile controller components
        projectileController = prefab.GetComponent<ProjectileController>();
        bossHealth = GetComponent<BossHealth>();

        // Create a new instance of the VulnerableStateTrigger custom node
        vulnerableStateTrigger = new VulnerableStateTrigger(bossHealth, projectileController, bulletHit, waitTime, damageToBoss, vulnerableBulletHit);

        // Create the behavior tree with a selector node as the root
        Node root = new Selector(new List<Node>
        {
            // The behavior tree has two branches:
            // 1. A sequence node to check for vulnerable state and apply damage if vulnerable
            // 2. A custom task node to make the boss hop between towers
            new Sequence(new List<Node>
            {
                vulnerableStateTrigger, // Check if the boss is in the vulnerable state and apply damage
                new VulnerableState(bossCollider, platformCollider), // Custom node to handle boss's vulnerable state
            }),
            new TaskTowerHop(transform, towers, rb, vulnerableStateTrigger), // Custom task node to make the boss hop between towers
        });

        return root;
    }
}
