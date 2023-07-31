using System.Collections.Generic;

namespace BehaviorTree
{
    // Sequence class that inherits from the Node class
    public class Sequence : Node
    {
        // Constructors for the Sequence node
        public Sequence() : base() { }

        public Sequence(List<Node> children) : base(children) { }

        // Override the Evaluate method to implement the behavior of the Sequence node
        public override NodeState Evaluate()
        {
            bool anyChildIsRunning = false;

            // Loop through all the children of the Sequence node
            foreach (Node node in children)
            {
                // Evaluate the child node and check its state
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        // If any child fails, the sequence fails too
                        state = NodeState.FAILURE;
                        return state;

                    case NodeState.SUCCESS:
                        // If the child succeeds, continue to the next child
                        continue;

                    case NodeState.RUNNING:
                        // If any child is running, set the flag to true
                        anyChildIsRunning = true;
                        continue;

                    default:
                        // If an unexpected state is encountered, consider the sequence as successful
                        state = NodeState.SUCCESS;
                        return state;
                }
            }

            // After evaluating all children, determine the state of the sequence node
            state = anyChildIsRunning ? NodeState.RUNNING : NodeState.SUCCESS;
            return state;
        }
    }
}
