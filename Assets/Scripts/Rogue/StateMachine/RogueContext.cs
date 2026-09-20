using Input;
using Running;
using Setting;
using UnityEngine;
using Utility;
namespace StateMachine.Rogue
{
    public class RogueContext
    {
        public InputManager InputManager;
        public PlayerProvideRegistry PlayerProvideRegistry { get; private set; }
        public ConfigRepository ConfigRepository { get; private set; }
        public MapBuilder MapBuilder { get; private set; }
        public PlayerCache PlayerCache { get; private set; }
        public PrefabLoader PrefabLoader { get; private set; }
        public PlayerFactory PlayerFactory { get; private set; }
        public Transform PlayerParent { get; private set; }
        public Transform RogueMapParent { get; private set; }
        public MapProgression MapProgression { get; private set; }
        public LayerProgression LayerProgression { get; private set; }
        public SettingService SettingService { get; private set; }
        public RogueCamera Camera { get; private set; }
        public void BindInputManager(InputManager inputManager)
        {
            if (inputManager == null)
            {
                Debug.LogError(
                    "RogueContext failed to bind InputManager: " +
                    "InputManager is null.");
                return;
            }

            InputManager = inputManager;
        }
        public void BindSettingService(SettingService settingService)
        {
            SettingService = settingService;
            if (SettingService == null)
            {
                Debug.LogError("SettingRepository is null");
            }
        }
        public void BindConfigRepository(ConfigRepository configRepository)
        {
            this.ConfigRepository = configRepository;
            if (ConfigRepository == null)
            {
                Debug.LogError("ConfigRepository is null");
            }
        }
        public void BindMapBuilder(MapBuilder mapBuilder)
        {
            this.MapBuilder = mapBuilder;
            if (MapBuilder == null)
            {
                Debug.LogError("MapBuilder is null");
            }
        }
        public void BindPlayerRegistry(PlayerProvideRegistry playerProvideRegistry)
        {
            this.PlayerProvideRegistry = playerProvideRegistry;
            if (PlayerProvideRegistry == null)
            {
                Debug.LogError("PlayerProviderRegistry is null");
            }
        }
        public void BindPlayerParent(Transform playerParent)
        {
            if (playerParent == null)
            {
                Debug.LogError("BindPlayerParent failed: PlayerParent is null.");
                return;
            }

            PlayerParent = playerParent;
        }
        public void BindRogueMapParent(Transform rogueMapParent)
        {
            if (rogueMapParent == null)
            {
                return;
            }
            RogueMapParent = rogueMapParent;
        }
        public void BindMapProgression(MapProgression mapProgression)
        {
            this.MapProgression = mapProgression;
            if (MapProgression == null)
            {
                Debug.LogError("MapProgression is null");
            }
        }
        public void BindLayerProgression(LayerProgression layerProgression)
        {
            this.LayerProgression = layerProgression;
        }
        public void BindRogueCamera(RogueCamera camera)
        {
            Camera = camera;
        }
    }
}
