using StateMachine.Node;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Running;
namespace StateMachine.Rogue
{
    public class NodeState : RogueState
    {
        private Running.Node CurrentNode;
        public NodeState(RogueStateMachine stateMachine, Running.Node currentNode) : base(stateMachine)
        {
            CurrentNode = currentNode;
            if (currentNode == null)
            {
                Debug.LogError("NodeState : CurrentNode is null !");
            }
        }
        public override void Enter()
        {
            base.Enter();
            EventBus.Subscript<ENodeCompleted>(OnNodeCompleted);
            if (CurrentNode != null)
            {
                CurrentNode.TryStart();
            }
        }
        public override void Exit()
        {
            base.Exit();
            EventBus.Unsubscript<ENodeCompleted>(OnNodeCompleted);
        }
        private void OnNodeCompleted(ENodeCompleted e)
        {
            StateMachine.PopState();
        }
    }
}