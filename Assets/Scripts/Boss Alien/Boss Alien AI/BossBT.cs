using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class BossBT : BehaviorTree.Tree
{
    public UnityEngine.Transform[] towers;

    public static float speed = 10f;

    public Rigidbody2D rb;
    //[SerializeField] privat GameObject 



    [SerializeField] private CircleCollider2D bulletCollider;
    [SerializeField] private CapsuleCollider2D bossCollider;
    [SerializeField] private BoxCollider2D paltformCollider;
    [SerializeField] private CircleCollider2D hitBox;
    public GameObject preFab;


    private static BossHealth bossHealth;
    private static ProjectileController projectileController;




    private static GameObject currentOnewayPlatform;

    private VulnerableStateTrigger vulnerableStateTrigger;







    protected override Node SetupTree()
    {
       

        projectileController = preFab.GetComponent<ProjectileController>();
        bossHealth = GetComponent<BossHealth>();

        vulnerableStateTrigger = new VulnerableStateTrigger(bossHealth, projectileController);

        Node root = new Selector(new List<Node>
        {
            new Sequence (new List<Node>
                {
                 vulnerableStateTrigger,
                new VulnerableState(currentOnewayPlatform,bossCollider,paltformCollider,hitBox),

            }) ,
            new TaskTowerHop(transform, towers,rb, vulnerableStateTrigger),
        });
        return root;




    }
}
    

     

  



   

