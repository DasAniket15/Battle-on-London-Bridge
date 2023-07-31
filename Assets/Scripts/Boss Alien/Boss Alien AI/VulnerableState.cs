using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class VulnerableState : Node
{
    private CapsuleCollider2D bossCollider;
    private BoxCollider2D platformCollider;

    private float counter = 0f;
    private bool collision = true ;
    private float _waitCounter = 0f;
    private float _waitTime = 5f;


    public VulnerableState(CapsuleCollider2D bossCollider, BoxCollider2D platformCollider)
    {
        this.bossCollider = bossCollider;
        this.platformCollider = platformCollider;
    }
    

    public override NodeState Evaluate()
    {
        if (collision == true)
        {
            Physics2D.IgnoreCollision(bossCollider, platformCollider);

            collision = false;

           state = NodeState.RUNNING;
            return state;
        }

        else
        {
            Physics2D.IgnoreCollision(bossCollider, platformCollider, false);

            collision = true;

            state = NodeState.RUNNING;

            return state;
        }
    }
}
