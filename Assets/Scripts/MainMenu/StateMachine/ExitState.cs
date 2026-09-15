namespace StateMachine.Core
{
    public class ExitState : CoreState
    {
        public ExitState(CoreStateMachine stateMachine) : base(stateMachine)
        {
        }
        public override void Enter()
        {
            if (StateMachine == null || StateMachine.Context == null)
            {
                return;
            }

            StateMachine.Context.OnExit();
        }
    }
}
