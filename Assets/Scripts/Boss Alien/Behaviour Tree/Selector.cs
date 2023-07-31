using System.Collections.Generic;

namespace BehaviorTree
{
    // Selector class that inherits from the Node class
    public class Selector : Node
    {
        // Constructors for the Selector node
        public Selector() : base() { }

        public Selector(List<Node> children) : base(children) { }

        // Override the Evaluate method to implement the behavior of the Selector node
        public override NodeState Evaluate()
        {
            // Loop through all the children of the Selector node
            foreach (Node node in children)
            {
                // Evaluate the child node and check its state
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        // If the child fails, continue to the next child
                        continue;

                    case NodeState.SUCCESS:
                        // If any child succeeds, the selector succeeds too
                        state = NodeState.SUCCESS;
                        return state;

                    case NodeState.RUNNING:
                        // If any child is running, the selector is also running
                        state = NodeState.RUNNING;
                        return state;

                    default:
                        // If an unexpected state is encountered, continue to the next child
                        continue;
                }
            }

            // If all children have been evaluated and none succeeded, the selector fails
            state = NodeState.FAILURE;
            return state;
        }
    }
}
