using Audio;
using Input;
using Utility;

namespace StateMachine.Rogue
{
    public class InitState : RogueState
    {
        //后续所有的Init逻辑可以移至RogueInitiallizer
        private RogueInitiallizer RogueInitiallizer;
        private AssetBundleManager AssetBundleManager;
        private AudioSystem AudioSystem;
        private InputManager InputManager;
        public InitState(
            RogueStateMachine stateMachine,
            AssetBundleManager assetBundleManager,
            AudioSystem audioSystem,
            InputManager inputManager) : base(stateMachine)
        {
            AssetBundleManager = assetBundleManager;
            AudioSystem = audioSystem;
            InputManager = inputManager;
        }
        public override void Enter()
        {
            base.Enter();
            Init();
            NewGame();
        }
        public override void Exit()
        {
            base.Exit();
        }
        private void Init()
        {
            RogueInitiallizer = new RogueInitiallizer(AssetBundleManager, AudioSystem, InputManager,StateMachine.Context.SettingService);
            RogueInitiallizer.InitContext(StateMachine.Context);
        }

        private void NewGame()
        {
            StateMachine.Context.LayerProgression.SetLayer(1);
            if (StateMachine.Context.ConfigRepository.TryGetMap(
                StateMachine.Context.LayerProgression.CurrentLayer, out Config.Map map))
            {
                StateMachine.SwitchState(new BuildMapState(StateMachine, map));
            }
        }
        private void Load()
        {

        }
    }
}
