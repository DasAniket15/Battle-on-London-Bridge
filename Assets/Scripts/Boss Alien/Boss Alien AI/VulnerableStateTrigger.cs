using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using Utils;

public class VulnerableStateTrigger : Node
{
    private BossHealth bossHealth;
    private ProjectileController projectileController;

    public bool vulnerability;
    private static float _waitCounter = 0f;
    private int currentHealth;
    public int collisionThreshold = 5;
    public bool vulnerabilityOver = false;
    private int bulletHit;
    private int waitTime;
    private int damageToBoss;


    public VulnerableStateTrigger(BossHealth bossHealth, ProjectileController projectileController, int bulletHit, int waitTime, int damageToBoss)
    { 
        this.bossHealth = bossHealth;
        this.projectileController = projectileController;
        this.bulletHit = bulletHit; 
        this.waitTime = waitTime;
        this.damageToBoss = damageToBoss;
        
    }


    public override NodeState Evaluate()
    {       
        currentHealth = bossHealth.GetCurrentHealth();

        Debug.Log(currentHealth);

        if (projectileController.GetCounter() == bulletHit)
        {
            vulnerability = true;

            projectileController.SetCounter(bulletHit +1);
            projectileController.SetDamage(damageToBoss);

            FunctionTimer.Create(vulnerableAction, waitTime);
            
            state = NodeState.SUCCESS;

            return state;
        }

        else if (vulnerabilityOver == true)
        {
            vulnerabilityOver = false;
            vulnerability = false;

            projectileController.SetCounter(0);
            projectileController.SetDamage(0);

            state = NodeState.RUNNING;

            return state;
        }
        
        else 
        {
            state = NodeState.FAILURE;

            return state;

        }
    }


    private void vulnerableAction() 
    {
        vulnerabilityOver = true;

        _waitCounter = _waitCounter + 1;

        Debug.Log("wait times" + _waitCounter);
    }
}
