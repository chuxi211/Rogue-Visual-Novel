using Running;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StateMachine.Core
{
    public class RogueModeState : CoreState
    {
        private EnterRogueMode EnterMode;
        private SavedInfo SavedInfo;
        private RogueBootstrap RogueBootstrap;
        public RogueModeState(CoreStateMachine stateMachine) : base(stateMachine)
        {
        }
        public RogueModeState(CoreStateMachine stateMachine,EnterRogueMode mode,SavedInfo savedInfo):base(stateMachine)
        {
            this.EnterMode = mode;
            this.SavedInfo = savedInfo;
        }
        public override void Enter()
        {
            base.Enter();
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene("Game");
            EventBus.Subscript<ERequestAbortRogue>(OnReturnMainMenu);
            EventBus.Subscript<ERequestSavedPanel>(OnRequestSavedPanel);
        }
        public override void Exit()
        {
            base.Exit();
            SceneManager.sceneLoaded -= OnSceneLoaded;
            EventBus.Unsubscript<ERequestAbortRogue>(OnReturnMainMenu);
            EventBus.Unsubscript<ERequestSavedPanel>(OnRequestSavedPanel);
            RogueBootstrap = null;
        }
        public override void Suspend()
        {
            EventBus.Unsubscript<ERequestAbortRogue>(OnReturnMainMenu);
            EventBus.Unsubscript<ERequestSavedPanel>(OnRequestSavedPanel);
        }
        public override void Resume()
        {
            EventBus.Subscript<ERequestAbortRogue>(OnReturnMainMenu);
            EventBus.Subscript<ERequestSavedPanel>(OnRequestSavedPanel);
        }
        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (scene.name == "Game")
            {
                SceneManager.sceneLoaded -= OnSceneLoaded;
                RogueBootstrap = GameObject.FindFirstObjectByType<RogueBootstrap>();
                if (RogueBootstrap != null)
                {
                    RogueBootstrap.InitStateMachine(StateMachine.Context,EnterMode,SavedInfo);
                }
            }
            if(scene.name == "MainMenu")
            {
                SceneManager.sceneLoaded -= OnSceneLoaded;
                StateMachine.SwitchState<MainMenuState>();
            }
        }
        private void OnRequestSavedPanel(ERequestSavedPanel e)
        {
            if(StateMachine.TryGetState<SavedModeState>(out var savedModeState))
            {
                if (e.ModeOfEnterSaveMode == ModeOfEnterSaveMode.Save && RogueBootstrap == null)
                {
                    Debug.LogError("Cannot save: RogueBootstrap is unavailable.");
                    return;
                }

                savedModeState.SetEnterMode(e.ModeOfEnterSaveMode);
                if (RogueBootstrap != null)
                {
                    savedModeState.SetSavedInfoProvider(RogueBootstrap.CreateSavedInfo);
                }
                else
                {
                    savedModeState.SetSavedInfoProvider(null);
                }
                StateMachine.SuspendState(savedModeState);
                return;
            }
        }
        private void OnReturnMainMenu(ERequestAbortRogue e)
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene("MainMenu");
        }
    }
}
