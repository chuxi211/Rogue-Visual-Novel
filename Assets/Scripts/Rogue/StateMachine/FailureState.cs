using UnityEngine;

namespace StateMachine.Rogue
{
    public class FailureState : RogueState
    {
        private readonly string Reason;
        public FailureState(RogueStateMachine stateMachine, string reason) : base(stateMachine)
        {
            Reason = reason;
        }
        public override void Enter()
        {
            base.Enter();
            Debug.LogError($"Rogue failed: {Reason}");
            StateMachine.SwitchState<ExitState>();
        }
    }
}
