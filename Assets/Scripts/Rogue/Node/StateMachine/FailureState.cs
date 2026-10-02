using Running;
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
            ClearAllVisual();
        }
        public override void Exit()
        {
            base.Exit();
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