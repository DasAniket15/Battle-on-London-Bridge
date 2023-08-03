using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;
using Utils;

public class DiveAttack : Node
{
    private VulnerableStateTrigger vulnerableStateTrigger;
    private Transform _transform; // Reference to the boss's transform
    private Transform[] _towers; // Array of tower transforms the boss will hop between
    private Transform transformHero; // transform of hero
    private int _currentTowerIndex = 0; // Index of the current tower the boss is hopping to
    private float _waitTime = 1f; // Time the boss waits on a tower before hopping to the next one
    private float _waitCounter = 0f; // Counter to track the waiting time
    private bool _waiting = false; // Flag to check if the boss is waiting on a tower
    private Rigidbody2D rb;
    public bool diveAttackHappening = false ;
    private static Vector3 diveTargetPosition;
    private ProjectileController projectileController;

    private CapsuleCollider2D bossCollider; // Capsule collider for the boss
    private BoxCollider2D platformCollider;

    public DiveAttack(VulnerableStateTrigger vulnerableStateTrigger, Transform[] towers, Transform transform, Rigidbody2D rb, ProjectileController projectileController,CapsuleCollider2D bossCollider, BoxCollider2D platformCollider)
    {
       this.vulnerableStateTrigger = vulnerableStateTrigger;
        this._transform = transform;
        this.rb = rb;
        this._towers = towers;
        this.projectileController = projectileController;
        this.bossCollider = bossCollider;
        this.platformCollider = platformCollider;   

    }

    public override NodeState Evaluate()
    {
        transformHero = rb.GetComponent<Transform>();
        Transform tower = _towers[1]; // Get the current target tower transform
        //FunctionTimer.Create(diveAction, 3f, "1");

        if (vulnerableStateTrigger.canDive == true)
        {
            // Calculate the distance between the boss and the hero
            float distanceToHero = Vector2.Distance(_transform.position, transformHero.position);
           
            diveAttackHappening = true;
           

            if (diveAttackHappening == true)
            {

                Physics2D.IgnoreCollision(bossCollider, platformCollider);
                    // Store the player's position as the dive target position
                diveTargetPosition = transformHero.position ;

                
                // Move towards the dive target position
                _transform.position = Vector2.MoveTowards(_transform.position, diveTargetPosition, BossBT.speed * Time.deltaTime);
               
                Debug.Log("again");
              
                if (distanceToHero <= 2f)
                {
                    Physics2D.IgnoreCollision(bossCollider, platformCollider, false);
                    vulnerableStateTrigger.canDive = false;
                    Debug.Log("Dive");
                    vulnerableStateTrigger.vulnerability = false;
                    vulnerableStateTrigger.phaseTwo = false;
                    projectileController.SetCounter(0);
                    vulnerableStateTrigger.nonVulnerabiltyOver = true;
                    diveAttackHappening = false;

                }



            }
            else
            {
                // The boss is very close to the hero, so the state should be FAILURE
               // _transform.position = Vector2.MoveTowards(_transform.position, tower.position, BossBT.speed * Time.deltaTime);

                Debug.Log("over");
            }
            state = NodeState.SUCCESS;
            return state;
        }
    
        else
        {
           // vulnerableStateTrigger.canDive = false;
            state = NodeState.FAILURE;
            return state;
        }


    }

    private void diveAction()
    {

       

        Debug.Log("Dive");
    }
}
