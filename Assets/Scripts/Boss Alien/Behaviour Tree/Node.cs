using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    // Enum to represent the state of a node in the behavior tree
    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }

    // Base class for creating nodes in the behavior tree
    public class Node
    {
        protected NodeState state; // Current state of the node

        public Node parent; // Reference to the parent node
        protected List<Node> children = new List<Node>(); // List of child nodes

        private Dictionary<string, object> _dataContext = new Dictionary<string, object>(); // Dictionary to store data related to the node

        // Default constructor for the node without any children
        public Node()
        {
            parent = null;
        }

        // Constructor for the node with a list of children
        public Node(List<Node> children)
        {
            foreach (Node child in children)
                _Attach(child);
        }

        // Method to attach a child node to this node
        private void _Attach(Node node)
        {
            node.parent = this;
            children.Add(node);
        }

        // Method to evaluate the node's behavior, to be overridden in derived classes
        public virtual NodeState Evaluate() => NodeState.FAILURE;

        // Method to set data in the node's data context
        public void SetData(string key, object value)
        {
            _dataContext[key] = value;
        }

        // Method to get data from the node's data context
        public object GetData(string key)
        {
            object value = null;

            if (_dataContext.TryGetValue(key, out value))
                return value;

            Node node = parent;

            while (node != null)
            {
                value = node.GetData(key);

                if (value != null)
                    return value;

                node = node.parent;
            }

            return null;
        }

        // Method to clear data from the node's data context
        public bool ClearData(string key)
        {
            if (_dataContext.ContainsKey(key))
            {
                _dataContext.Remove(key);
                return true;
            }

            Node node = parent;

            while (node != null)
            {
                bool cleared = node.ClearData(key);

                if (cleared)
                    return true;

                node = node.parent;
            }

            return false;
        }

        // Method to get the current state of the node
        public virtual NodeState GetState()
        {
            return state;
        }

        // Method to set the state of the node
        public void SetState(NodeState newState)
        {
            state = newState;
        }
    }
}
