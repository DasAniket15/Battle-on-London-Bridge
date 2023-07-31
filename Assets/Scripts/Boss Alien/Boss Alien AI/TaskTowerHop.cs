using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskTowerHop : Node
{
    private Transform _transform; // Reference to the boss's transform
    private Transform[] _towers; // Array of tower transforms the boss will hop between
    private int _currentTowerIndex = 0; // Index of the current tower the boss is hopping to
    private float _waitTime = 1f; // Time the boss waits on a tower before hopping to the next one
    private float _waitCounter = 0f; // Counter to track the waiting time
    private bool _waiting = false; // Flag to check if the boss is waiting on a tower
    public Rigidbody2D rb; // Rigidbody2D component of the boss
    private VulnerableStateTrigger vulnerableStateTrigger; // Reference to the vulnerable state trigger custom node

    public TaskTowerHop(Transform transform, Transform[] towers, Rigidbody2D rb, VulnerableStateTrigger vulnerableStateTrigger)
    {
        _transform = transform; // Initialize the boss's transform
        _towers = towers; // Initialize the tower transforms array
        this.rb = rb; // Initialize the boss's Rigidbody2D component
        this.vulnerableStateTrigger = vulnerableStateTrigger; // Initialize the vulnerable state trigger custom node
    }

    // Override the Evaluate method to define the behavior of the boss hopping between towers
    public override NodeState Evaluate()
    {
        if (vulnerableStateTrigger.vulnerability == false)
        {
            // If the boss is waiting, increment the wait counter and check if the waiting time is over
            if (_waiting)
            {
                _waitCounter += Time.deltaTime;

                if (_waitCounter >= _waitTime)
                {
                    _waiting = false;
                }
            }
            else
            {
                Transform tower = _towers[_currentTowerIndex]; // Get the current target tower transform

                // If the boss is close enough to the target tower, set the boss's position to the tower's position,
                // reset the wait counter, and set the waiting flag to true.
                if (Vector2.Distance(_transform.position, tower.position) < 0.01f)
                {
                    _transform.position = tower.position;
                    _waitCounter = 0f;
                    _waiting = true;

                    // Move to the next tower in the array for the next hop (looping back to the first tower if needed)
                    _currentTowerIndex = (_currentTowerIndex + 1) % _towers.Length;
                }
                else
                {
                    // Move the boss towards the target tower using the specified speed
                    _transform.position = Vector2.MoveTowards(_transform.position, tower.position, BossBT.speed * Time.deltaTime);
                }
            }

            state = NodeState.RUNNING; // The node is still running

            return state;
        }
        else
        {
            state = NodeState.FAILURE; // The node fails if the boss is vulnerable

            return state;
        }
    }
}
