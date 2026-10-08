using Running;
using UnityEngine;
namespace StateMachine.Node
{
    public class FailureState : NodeState
    {
        private readonly NodeErrorCode ErrorCode;
        public FailureState(NodeStateMachine stateMachine,NodeErrorCode errorCode) : base(stateMachine)
        {
            ErrorCode = errorCode;
        }
        public override void Enter()
        {
            base.Enter();
            Debug.LogError($"Node '{StateMachine.Node?.ID}' failed: {ErrorCode}.");
            ClearAllVisual();
            EventBus.Publish(new ENodeFailed(StateMachine.Node?.ID, ErrorCode));
        }
        public override void Exit()
        {
            base.Exit();
        }
        private void ClearAllVisual()
        {
            if (StateMachine.Node == null)
            {
                return;
            }
            while (StateMachine.Node.EnteredComponents.Count > 0)
            {
                IComponent cmp = StateMachine.Node.EnteredComponents.Dequeue();
                if (cmp == null) continue;
                try
                {
                    cmp.ExitOnNodeEnd();
                }
                catch (System.Exception exception)
                {
                    Debug.LogException(exception);
                }
            }
        }
    }
}
