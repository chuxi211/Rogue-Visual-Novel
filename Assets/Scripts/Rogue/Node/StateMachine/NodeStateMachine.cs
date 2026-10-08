using Config;
using Running;
using UnityEngine;
using Utility;
namespace StateMachine.Node
{
    //实际上，应该是NodeAdvancer
    //这是早期的设计失误，为每一个Node绑定一个StateMachine
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
            return TryStart(out _);
        }
        public bool TryStart(out NodeErrorCode errorCode)
        {
            errorCode = NodeErrorCode.None;
            if (Node == null)
            {
                errorCode = NodeErrorCode.InvalidNode;
                return false;
            }
            if (Node.Steps == null || Node.Steps.Count <= 0)
            {
                errorCode = NodeErrorCode.NoSteps;
                return false;
            }
            if (CurrentStepIndex < 0 || CurrentStepIndex >= Node.Steps.Count)
            {
                errorCode = NodeErrorCode.InvalidStartStep;
                return false;
            }
            CurrentStep = Node.Steps[CurrentStepIndex];
            if (CurrentStep == null || CurrentStep.AdvanceType != StepAdvanceType.Start)
            {
                errorCode = NodeErrorCode.InvalidStartStep;
                return false;
            }
            SwitchState(new AdvanceState(this, CurrentStep));
            return true;
        }
        public void Advance(StepID nextID)
        {
            switch(Node.TryGetStep(nextID,out Running.Step nextStep))
            {
                case Running.GetStepCode.Succeeded:
                    CurrentStep = nextStep;
                    CurrentStepIndex = Node.Steps.IndexOf(nextStep);
                    SwitchState(new AdvanceState(this, CurrentStep));
                    break;
                case Running.GetStepCode.Failed:
                    SwitchState(new FailureState(this,Running.NodeErrorCode.StepNotFound));
                    break;
                case Running.GetStepCode.InvalidID:
                    SwitchState(new FailureState(this, Running.NodeErrorCode.InvalidNextStepID));
                    break;
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
