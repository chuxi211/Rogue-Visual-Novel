namespace StateMachine.Rogue
{
    public class ExitState : RogueState
    {
        public ExitState(RogueStateMachine stateMachine) : base(stateMachine)
        {
        }
        public override void Enter()
        {
            base.Enter();
            EventBus.Publish(new ERequestAbortRogue());
        }
    }
}