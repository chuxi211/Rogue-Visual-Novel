using Input;
using UnityEngine;
namespace StateMachine.Rogue
{
    public class IdleState : RogueState
    {
        public IdleState(RogueStateMachine stateMachine) : base(stateMachine)
        {
        }
        public override void Enter()
        {
            base.Enter();

            InputManager inputManager = StateMachine?.Context?.InputManager;
            if (inputManager == null)
            {
                StateMachine.SwitchState(new FailureState(StateMachine, "Rogue input manager is unavailable."));
                return;
            }

            inputManager.Switch(new Input.RogueInputState());
            EventBus.Subscript<ERequestEnterNode>(OnRequestEnterNode);
        }
        public override void Exit()
        {
            EventBus.Unsubscript<ERequestEnterNode>(OnRequestEnterNode);
            base.Exit();
        }
        private void OnRequestEnterNode(ERequestEnterNode e)
        {
            if (e?.Actor == null || e.Actor.NodeRunning == null ||
                StateMachine.Context.MapProgression == null || StateMachine.Context.Camera == null)
            {
                StateMachine.SwitchState(new FailureState(StateMachine, "Node entry dependencies are missing."));
                return;
            }
            if (!StateMachine.Context.MapProgression.TryMoveTo(e.Actor.NodeRunning))
            {
                return;
            }
            StateMachine.Context.Camera.StartMoveTo(e.Actor.gameObject.transform.position);
            StateMachine.PushState(new NodeState(StateMachine, e.Actor.NodeRunning));
        }
    }
}
