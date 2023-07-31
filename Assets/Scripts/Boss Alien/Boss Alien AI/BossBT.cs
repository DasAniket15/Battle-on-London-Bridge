using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class BossBT : BehaviorTree.Tree
{
    public UnityEngine.Transform[] towers;

    public static float speed = 10f;

    public Rigidbody2D rb;

    [SerializeField] private CircleCollider2D bulletCollider;
    [SerializeField] private CapsuleCollider2D bossCollider;
    [SerializeField] private BoxCollider2D platformCollider;

    [SerializeField] private int bulletHit;
    [SerializeField] private int waitTime;
    [SerializeField] private int damageToBoss;

    public GameObject prefab;

    private static BossHealth bossHealth;
    private static ProjectileController projectileController;

    private VulnerableStateTrigger vulnerableStateTrigger;


    protected override Node SetupTree()
    {
        projectileController = prefab.GetComponent<ProjectileController>();
        bossHealth = GetComponent<BossHealth>();

        vulnerableStateTrigger = new VulnerableStateTrigger(bossHealth, projectileController, bulletHit, waitTime, damageToBoss);

        Node root = new Selector(new List<Node>
        {
            new Sequence (new List<Node>
                {
                 vulnerableStateTrigger,
                new VulnerableState(bossCollider, platformCollider),
            }) ,
            
            new TaskTowerHop(transform, towers, rb, vulnerableStateTrigger),
        });

        return root;
    }
}
