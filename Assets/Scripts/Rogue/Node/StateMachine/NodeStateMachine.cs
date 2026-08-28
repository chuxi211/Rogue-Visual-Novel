using Config;
using UnityEngine;
using Utility;
namespace StateMachine.Node
{
    public class NodeStateMachine : StateMachine<NodeState>
    {
        public Running.Node Node { get; private set; }
        public Running.Step CurrentStep { get; set; }
        public int CurrentStepIndex { get; set; } = 0;
        public NodeStateMachine(Running.Node node)
        {
            Node = node;
            if (Node == null)
            {
                Debug.LogError("Node is null");
            }
        }
        public bool TryStart()
        {
            if (Node.Steps.Count > 0)
            {
                CurrentStep = Node.Steps[CurrentStepIndex];
                SwitchState(new AdvanceState(this,CurrentStep));
                return true;
            }
            else
            {
                Debug.LogError("Node has no steps");
                return false;
            }
        }
        public void Advance(StepID nextID)
        {
            if(Node.StepsMap.TryGetValue(nextID, out Running.Step nextStep))
            {
                CurrentStep = nextStep;
                CurrentStepIndex = Node.Steps.IndexOf(nextStep);
                SwitchState(new AdvanceState(this, CurrentStep));
            }
            else
            {
                Debug.LogError($"No step with ID {nextID} found in the node.\nThe Node will be interrupted");
                Complete();
            }
        }
        public void Complete()
        {
            CurrentStep = null;
            CurrentStepIndex = 0;
            Node.MarkCompleted();
            SwitchState(new ExitState(this));
        }
    }
}