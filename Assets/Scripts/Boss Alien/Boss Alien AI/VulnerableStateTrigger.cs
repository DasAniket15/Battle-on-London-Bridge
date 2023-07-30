using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class VulnerableStateTrigger : Node
{
    public bool vulnerability;
    private float _waitCounter = 0f;
    private BossHealth bossHealth;
    private ProjectileController projectileController;
    private int currentHealth;

    public VulnerableStateTrigger(BossHealth bossHealth, ProjectileController projectileController)
    { 
        this.bossHealth = bossHealth;
        this.projectileController = projectileController;
    }

    public override NodeState Evaluate()
    {
        _waitCounter += Time.deltaTime;
        currentHealth = bossHealth.GetCurrentHealth();
        //bossHealth.SetDamage(20);
        Debug.Log(currentHealth);
        projectileController.damage = 10;

     
        if (currentHealth == 800)

        {
            vulnerability = true;
            bossHealth.SetCurrentHealth(790);
           
            Debug.Log("hi");
            state = NodeState.SUCCESS;
            return state;
        }
        else if (currentHealth == 600)
        {
            bossHealth.SetCurrentHealth(590);
            vulnerability = false;

            state = NodeState.RUNNING;
            return state;

        }

        else if (currentHealth == 400)
        {

            vulnerability = true;
            Debug.Log("hi");
            bossHealth.SetCurrentHealth(390);

            state = NodeState.SUCCESS;
            return state;


        }
        else if (currentHealth == 200)
        {
            bossHealth.SetCurrentHealth(190);
            vulnerability = false;
            Debug.Log("hi");

            state = NodeState.SUCCESS;
            return state;


        }
        else 
        {
            state = NodeState.FAILURE;
            return state;

        }

    }
    }
