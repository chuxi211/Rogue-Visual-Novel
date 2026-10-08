using Audio;
using Input;
using Running;
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
        private SavedInfo SavedInfo = null;
        public InitState(
            RogueStateMachine stateMachine,
            AssetBundleManager assetBundleManager,
            AudioSystem audioSystem,
            InputManager inputManager,
            EnterRogueMode mode,
            SavedInfo savedInfo=null) : base(stateMachine)
        {
            AssetBundleManager = assetBundleManager;
            AudioSystem = audioSystem;
            InputManager = inputManager;
            EnterRogueMode = mode;
            SavedInfo = savedInfo;
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
                    StateMachine.SwitchState(new FailureState(StateMachine, "Initial map could not be loaded."));
                    break;
                case GetMapCode.InvaildLayer:
                    StateMachine.SwitchState(new FailureState(StateMachine, "Initial map layer is invalid."));
                    break;
                case GetMapCode.Ended:
                    StateMachine.SwitchState(new ExitState(StateMachine));
                    break;
            }
        }
        private void Load()
        {
            if (SavedInfo == null || !SavedInfo.IsValid)
            {
                StateMachine.SwitchState(new FailureState(StateMachine, "Save data is missing or invalid."));
                return;
            }
            StateMachine.Context.LayerProgression.SetLayer(SavedInfo.CurrentLayer);
            switch ( StateMachine.Context.ConfigRepository.TryGetMap(
                SavedInfo.CurrentMapID,out Config.Map map))
            {
                case GetMapCode.SucceededToGet:
                    if (!ContainsSavedNode(map))
                    {
                        StateMachine.SwitchState(new FailureState(
                            StateMachine, $"Saved node '{SavedInfo.CurrentNodeID}' was not found in the map."));
                        break;
                    }
                    StateMachine.SwitchState(new BuildMapState(StateMachine, map));
                    break;
                case GetMapCode.FailedToGet:
                    StateMachine.SwitchState(new FailureState(StateMachine, $"Saved map '{SavedInfo.CurrentMapID.Value}' could not be loaded."));
                    break;
                case GetMapCode.InvaildMapID:
                    StateMachine.SwitchState(new FailureState(StateMachine, "Saved map ID is invalid."));
                    break;
                case GetMapCode.Ended:
                    StateMachine.SwitchState(new ExitState(StateMachine));
                    break;
            }
            return;
        }
        private bool ContainsSavedNode(Config.Map map)
        {
            if (map?.Nodes == null)
            {
                return false;
            }
            foreach (Config.Node node in map.Nodes)
            {
                if (node != null && node.NodeID == SavedInfo.CurrentNodeID)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
