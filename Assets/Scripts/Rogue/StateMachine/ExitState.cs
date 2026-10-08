using Input;

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
            StateMachine.Context.InputManager?.Switch(new IdleInputState());
            EventBus.Publish(new ERequestAbortRogue());
        }
    }
}
