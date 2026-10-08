using Running;
using System;
using UnityEngine;

namespace StateMachine.Node
{
    public class ExitState : NodeState
    {
        public ExitState(NodeStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            try
            {
                ClearAllVisual();
                EventBus.Publish(new ENodeCompleted());
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
                StateMachine.SwitchState(new FailureState(StateMachine, NodeErrorCode.InvalidComponent));
            }
        }
        private void ClearAllVisual()
        {
            while (StateMachine.Node.EnteredComponents.Count > 0)
            {
                IComponent cmp = StateMachine.Node.EnteredComponents.Dequeue();
                cmp.ExitOnNodeEnd();
            }
        }
    }
}
