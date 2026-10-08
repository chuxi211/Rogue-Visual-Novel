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
                Debug.LogError(
                    "Rogue NodeState failed to enter: InputManager is null.");
                return;
            }

            inputManager.Switch(new StepInputState(StateMachine.Context.SettingService));
            EventBus.Subscript<ENodeCompleted>(OnNodeCompleted);
            if (CurrentNode == null || !CurrentNode.TryStart())
            {
                StateMachine.SwitchState(new FailureState(StateMachine));
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
                int currentlayer = StateMachine.Context.LayerProgression.CurrentLayer;
                int maxlayer = StateMachine.Context.LayerProgression.MaxLayer;
                switch (StateMachine.Context.ConfigRepository.TryGetMap(currentlayer + 1, maxlayer, out var map))
                {
                    case GetMapCode.SucceededToGet:
                        StateMachine.SwitchState(new BuildMapState(StateMachine, map));
                        StateMachine.Context.LayerProgression.AddLayer();
                        break;
                    case GetMapCode.FailedToGet:
                        StateMachine.SwitchState(new FailureState(StateMachine));
                        break;
                    case GetMapCode.InvaildLayer:
                        StateMachine.SwitchState(new FailureState(StateMachine));
                        break;
                    case GetMapCode.Ended:
                        StateMachine.SwitchState<ExitState>();
                        break;

                }
            }
        }
    }
}
