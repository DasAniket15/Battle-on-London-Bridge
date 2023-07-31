using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Define the BehaviorTree namespace to encapsulate the behavior tree classes
namespace BehaviorTree
{
    // The abstract base class for all behavior trees
    public abstract class Tree : MonoBehaviour
    {
        // The root node of the behavior tree
        private Node _root = null;

        // Called at the start to set up the behavior tree
        protected void Start()
        {
            // Call the SetupTree method to create the behavior tree
            _root = SetupTree();
        }

        // Called every frame to evaluate the behavior tree
        private void Update()
        {
            // Check if the root node exists and then evaluate the behavior tree
            if (_root != null)
                _root.Evaluate();
        }

        // Abstract method to be implemented in derived classes to construct the behavior tree
        protected abstract Node SetupTree();
    }
}
