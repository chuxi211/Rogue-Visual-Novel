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
            SceneManager.sceneLoaded += OnGameSceneLoaded;
            SceneManager.LoadScene("Game");
        }
        private void OnGameSceneLoaded(Scene scene,LoadSceneMode loadSceneMode)
        {
            if (scene.name == "Game")
            {
                SceneManager.sceneLoaded -= OnGameSceneLoaded;
            }
        }
    }
}