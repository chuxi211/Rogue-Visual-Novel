using Running;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StateMachine.Core
{
    public class RogueModeState : CoreState
    {
        private EnterRogueMode EnterMode;
        private SavedInfo SavedInfo;
        private RogueBootstrap RogueBootstrap;
        private bool ReturningToMenu;
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
            ReturningToMenu = false;
            SceneManager.sceneLoaded += OnSceneLoaded;
            EventBus.Subscript<ERequestAbortRogue>(OnReturnMainMenu);
            EventBus.Subscript<ERequestSavedPanel>(OnRequestSavedPanel);
            SceneManager.LoadScene("Game");
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
            EventBus.Unsubscript<ERequestSavedPanel>(OnRequestSavedPanel);
        }
        public override void Resume()
        {
            EventBus.Subscript<ERequestSavedPanel>(OnRequestSavedPanel);
        }
        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (scene.name == "Game")
            {
                SceneManager.sceneLoaded -= OnSceneLoaded;
                RogueBootstrap = GameObject.FindFirstObjectByType<RogueBootstrap>();
                if (RogueBootstrap == null ||
                    !RogueBootstrap.InitStateMachine(StateMachine.Context,EnterMode,SavedInfo))
                {
                    Debug.LogError("Rogue scene initialization failed.");
                    OnReturnMainMenu(new ERequestAbortRogue());
                }
            }
            if(scene.name == "MainMenu")
            {
                SceneManager.sceneLoaded -= OnSceneLoaded;
                StateMachine.ClearHistory();
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
            if (ReturningToMenu)
            {
                return;
            }
            ReturningToMenu = true;
            EventBus.Unsubscript<ERequestAbortRogue>(OnReturnMainMenu);
            EventBus.Unsubscript<ERequestSavedPanel>(OnRequestSavedPanel);
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneLoaded += OnSceneLoaded;
            if (CoreBootstrap.Instance != null)
            {
                CoreBootstrap.Instance.StartCoroutine(LoadMainMenuNextFrame());
            }
            else
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
        private IEnumerator LoadMainMenuNextFrame()
        {
            yield return null;
            SceneManager.LoadScene("MainMenu");
        }
    }
}
