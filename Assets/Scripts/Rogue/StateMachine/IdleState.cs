using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Input;
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
                Debug.LogError(
                    "Rogue IdleState failed to enter: InputManager is null.");
                return;
            }

            inputManager.Switch(new MapInputState());
            EventBus.Subscript<ERequestEnterNode>(OnRequestEnterNode);
        }
        public override void Exit()
        {
            EventBus.Unsubscript<ERequestEnterNode>(OnRequestEnterNode);
            base.Exit();
        }
        private void OnRequestEnterNode(ERequestEnterNode e)
        {
            if (!StateMachine.Context.MapProgression.TryMoveTo(e.Actor.NodeRunning))
            {
                return;
            }
            StateMachine.Context.Camera.StartMoveTo(e.Actor.gameObject.transform.position);
            StateMachine.PushState(new NodeState(StateMachine,e.Actor.NodeRunning));
        }
    }
}
