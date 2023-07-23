using System.Collections.Generic;
using BehaviorTree;

public class BossBT : Tree
{
    public UnityEngine.Transform[] towers;

    public static float speed = 10f;
    

    protected override Node SetupTree()
    {
        Node root = new TaskTowerHop(transform, towers);

        return root;
    }
}
