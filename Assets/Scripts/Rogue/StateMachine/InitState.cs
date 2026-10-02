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
        private EnterRogueMode EnterRogueMode;
        private int LoadIndex;
        public InitState(
            RogueStateMachine stateMachine,
            AssetBundleManager assetBundleManager,
            AudioSystem audioSystem,
            InputManager inputManager,
            EnterRogueMode mode,
            int index) : base(stateMachine)
        {
            AssetBundleManager = assetBundleManager;
            AudioSystem = audioSystem;
            InputManager = inputManager;
            EnterRogueMode = mode;
            LoadIndex = index;
        }
        public override void Enter()
        {
            base.Enter();
            Init();
            switch (EnterRogueMode)
            {
                case EnterRogueMode.New:
                    NewGame(); break;
                case EnterRogueMode.Load:
                    Load(); break;
            }
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
            switch(StateMachine.Context.ConfigRepository.TryGetMap(
                StateMachine.Context.LayerProgression.CurrentLayer,
                StateMachine.Context.LayerProgression.MaxLayer,
                out Config.Map map))
            {
                case GetMapCode.SucceededToGet:
                    StateMachine.SwitchState(new BuildMapState(StateMachine, map));
                    break;
                case GetMapCode.FailedToGet:
                    StateMachine.SwitchState(new FailureState(StateMachine));
                    break;
                case GetMapCode.InvaildLayer:
                    StateMachine.SwitchState(new FailureState(StateMachine));
                    break;
                case GetMapCode.Ended:
                    StateMachine.SwitchState(new ExitState(StateMachine));
                    break;
            }
        }
        private void Load()
        {
            UnityEngine.Debug.LogError("LoadSystem is not Implement");
            return;
        }
    }
}
