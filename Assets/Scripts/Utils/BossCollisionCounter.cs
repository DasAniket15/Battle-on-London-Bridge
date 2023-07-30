using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCollisionCounter : MonoBehaviour
{
    public static BossCollisionCounter Instance { get; private set; }
    public int BossCollisionCount { get; private set; }

    private void Awake()
    {
       
        
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void IncreaseCollisionCount()
    {
        BossCollisionCount++;
    }
}
