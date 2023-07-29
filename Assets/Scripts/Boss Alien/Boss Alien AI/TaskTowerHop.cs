using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskTowerHop : Node
{
    private Transform _transform;
    // private Animator _animator;
    private Transform[] _towers;

    private int _currentTowerIndex = 0;
    private float _waitTime = 1f; // in seconds
    private float _waitCounter = 0f;
    private bool _waiting = false;


    public TaskTowerHop(Transform transform, Transform[] towers)
    {
        _transform = transform;
        // _animator = transform.GetComponent<Animator>();
        _towers = towers;
    }


    public override NodeState Evaluate()
    {
        if (_waiting)
        {
            _waitCounter += Time.deltaTime;

            if (_waitCounter >= _waitTime)
            {
                _waiting = false;
                // _animator.SetBool("Jump", true);
            }
        }

        else
        {
            Transform tower = _towers[_currentTowerIndex];
            if (Vector2.Distance(_transform.position, tower.position) < 0.01f)
            {
                _transform.position = tower.position;
                _waitCounter = 0f;
                _waiting = true;

                _currentTowerIndex = (_currentTowerIndex + 1) % _towers.Length;
                // _animator.SetBool("Jump", false);
            }
            else
            {
                _transform.position = Vector2.MoveTowards(_transform.position, tower.position, BossBT.speed * Time.deltaTime);
            }
        }

        state = NodeState.RUNNING;

        return state;
    }
}
