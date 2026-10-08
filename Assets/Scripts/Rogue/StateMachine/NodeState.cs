using Input;
using Running;
using UnityEngine;
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

            InputManager inputManager = StateMachine?.Context?.InputManager;
            if (inputManager == null)
            {
                StateMachine.SwitchState(new FailureState(StateMachine, "Node input manager is unavailable."));
                return;
            }

            inputManager.Switch(new StepInputState(StateMachine.Context.SettingService));
            EventBus.Subscript<ENodeCompleted>(OnNodeCompleted);
            EventBus.Subscript<ENodeFailed>(OnNodeFailed);
            if (CurrentNode == null)
            {
                StateMachine.SwitchState(new FailureState(StateMachine, "Current node is null."));
                return;
            }
            if (!CurrentNode.TryStart(out NodeErrorCode errorCode))
            {
                StateMachine.SwitchState(new FailureState(
                    StateMachine, $"Cannot start node '{CurrentNode.ID}': {errorCode}."));
            }
        }
        public override void Exit()
        {
            base.Exit();
            EventBus.Unsubscript<ENodeCompleted>(OnNodeCompleted);
            EventBus.Unsubscript<ENodeFailed>(OnNodeFailed);
        }
        private void OnNodeFailed(ENodeFailed e)
        {
            if (CurrentNode != null && e.NodeID == CurrentNode.ID)
            {
                StateMachine.SwitchState(new FailureState(
                    StateMachine, $"Node '{e.NodeID}' failed: {e.ErrorCode}."));
            }
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
                int currentlayer = StateMachine.Context.LayerProgression.CurrentLayer;
                int maxlayer = StateMachine.Context.LayerProgression.MaxLayer;
                switch (StateMachine.Context.ConfigRepository.TryGetMap(currentlayer + 1, maxlayer, out var map))
                {
                    case GetMapCode.SucceededToGet:
                        StateMachine.SwitchState(new BuildMapState(StateMachine, map));
                        StateMachine.Context.LayerProgression.AddLayer();
                        break;
                    case GetMapCode.FailedToGet:
                        StateMachine.SwitchState(new FailureState(StateMachine, "Next map could not be loaded."));
                        break;
                    case GetMapCode.InvaildLayer:
                        StateMachine.SwitchState(new FailureState(StateMachine, "Next map layer is invalid."));
                        break;
                    case GetMapCode.Ended:
                        StateMachine.SwitchState<ExitState>();
                        break;

                }
            }
        }
    }
}
