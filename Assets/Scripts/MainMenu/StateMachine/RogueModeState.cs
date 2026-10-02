using UnityEngine.SceneManagement;

namespace StateMachine.Core
{
    public class RogueModeState : CoreState
    {
        private EnterRogueMode EnterMode;
        private int LoadIndex;
        public RogueModeState(CoreStateMachine stateMachine) : base(stateMachine)
        {
        }
        public RogueModeState(CoreStateMachine stateMachine,EnterRogueMode mode,int index):base(stateMachine)
        {
            this.EnterMode = mode;
            this.LoadIndex = index;
        }
        public override void Enter()
        {
            base.Enter();
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene("Game");
            EventBus.Subscript<ERequestAbortRogue>(OnReturnMainMenu);
        }
        public override void Exit()
        {
            base.Exit();
            SceneManager.sceneLoaded -= OnSceneLoaded;
            EventBus.Unsubscript<ERequestAbortRogue>(OnReturnMainMenu);
        }
        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (scene.name == "Game")
            {
                SceneManager.sceneLoaded -= OnSceneLoaded;
                RogueBootstrap rogueBootstrap = UnityEngine.GameObject.FindFirstObjectByType<RogueBootstrap>();
                if (rogueBootstrap != null)
                {
                    rogueBootstrap.InitStateMachine(StateMachine.Context,EnterMode,LoadIndex);
                }
            }
            if(scene.name == "MainMenu")
            {
                SceneManager.sceneLoaded -= OnSceneLoaded;
                StateMachine.SwitchState<MainMenuState>();
            }
        }
        private void OnReturnMainMenu(ERequestAbortRogue e)
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene("MainMenu");
        }
    }
}
