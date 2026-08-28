using StateMachine.Node;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Running;
using Config;
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
            //若当前图未完成，返回
            if (!StateMachine.Context.MapProgression.IsMapCompleted(CurrentNode))
            {
                return;
            }
            //若完成，执行以下操作
            else
            {
                int currentlayer=StateMachine.Context.LayerProgression.CurrentLayer;
                if(StateMachine.Context.ConfigRepository.TryGetMap(currentlayer+1, out var map))
                {
                    StateMachine.SwitchState(new BuildMapState(StateMachine, map));
                    StateMachine.Context.LayerProgression.AddLayer();
                }
                else
                {
                    Debug.LogError("Fatal Error: Fail to get next map.");
                }
            }
        }
    }
}