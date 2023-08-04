using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class LaserAttack : Node

{
    public bool laser = false;
    private VulnerableStateTrigger vulnerableStateTrigger;
    public LaserAttack( VulnerableStateTrigger vulnerableStateTrigger) 
    { 
        this.vulnerableStateTrigger = vulnerableStateTrigger;
    }

    public override NodeState Evaluate()
    {


        if (vulnerableStateTrigger.phaseTwo == true)
        {
            state = NodeState.FAILURE;
            return state;
        }
        else 
        {
            state = NodeState.FAILURE; 
            return state;
        }

    }
}
