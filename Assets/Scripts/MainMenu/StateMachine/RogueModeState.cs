using UnityEngine.SceneManagement;

namespace StateMachine.Core
{
    public class RogueModeState : CoreState
    {
        public RogueModeState(CoreStateMachine stateMachine) : base(stateMachine)
        {
        }
        public override void Enter()
        {
            base.Enter();
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene("Game");
        }
        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (scene.name == "Game")
            {
                SceneManager.sceneLoaded -= OnSceneLoaded;
                RogueBootstrap rogueBootstrap = UnityEngine.GameObject.FindFirstObjectByType<RogueBootstrap>();
                if (rogueBootstrap != null)
                {
                    rogueBootstrap.InitStateMachine(StateMachine.Context);
                }
            }
        }
    }
}
