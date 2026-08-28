using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace StateMachine.Core
{
    public class MainMenuState : CoreState
    {
        public MainMenuState(CoreStateMachine stateMachine) : base(stateMachine)
        {
        }
        public override void Enter()
        {
            EventBus.Subscript<RequestNewGameEvent>(OnNewGame);
        }
        public override void Exit()
        {
            EventBus.Unsubscript<RequestNewGameEvent>(OnNewGame);
        }
        private void OnNewGame(RequestNewGameEvent e)
        {
            StateMachine.SwitchState<RogueModeState>();
        }
    }
}