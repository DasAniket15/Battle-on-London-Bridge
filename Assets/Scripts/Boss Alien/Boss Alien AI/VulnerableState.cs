using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class VulnerableState : Node
{

    private GameObject currentOnewayPlatform;
    private CapsuleCollider2D bossCollider;
    private BoxCollider2D paltformCollider;
    private CircleCollider2D hitBox; 

    private float counter = 0f;
    private bool collision = true ;
    private float _waitCounter = 0f;
    private float _waitTime = 5f;


    public VulnerableState(GameObject currentOnewayPlatform, CapsuleCollider2D bossCollider, BoxCollider2D paltformCollider, CircleCollider2D hitBox)
    {
        this.currentOnewayPlatform = currentOnewayPlatform;
        this.bossCollider = bossCollider;
        this.paltformCollider = paltformCollider;
        this.hitBox = hitBox;

    }
    
    public override NodeState Evaluate()
    {
        
        
        
    if (collision == true)
        {
             // Make the boss ignore collision with the specified platform
            Physics2D.IgnoreCollision(bossCollider, paltformCollider);
            Physics2D.IgnoreCollision(hitBox, paltformCollider);

            collision = false;
            Debug.Log("1");
            

           state = NodeState.RUNNING;
            return state;




        }
        else
        {
            Physics2D.IgnoreCollision(bossCollider, paltformCollider, false);

            collision = true;
            Debug.Log("2");


            state = NodeState.RUNNING;
            return state;

        }
       
        
        // If not in the vulnerable state, do nothing




    }

   
}
