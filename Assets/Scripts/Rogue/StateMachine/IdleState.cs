using System.Collections;
using System.Collections.Generic;
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
            InputManager.Instance.InputStateMachine.SwitchState(new MapInputState());
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
