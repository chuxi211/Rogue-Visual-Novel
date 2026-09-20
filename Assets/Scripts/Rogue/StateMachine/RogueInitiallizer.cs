using Audio;
using Config;
using Input;
using Running;
using Setting;
using StateMachine.Rogue;
using UnityEngine;
using Utility;

public class RogueInitiallizer
{
    private readonly AssetBundleManager AssetBundleManager;
    private readonly AudioSystem AudioSystem;
    private readonly InputManager InputManager;
    private readonly SettingService SettingService;
    public RogueInitiallizer(
        AssetBundleManager assetBundleManager,
        AudioSystem audioSystem,
        InputManager inputManager,
        SettingService settingService)
    {
        AssetBundleManager = assetBundleManager;
        AudioSystem = audioSystem;
        InputManager = inputManager;
        SettingService = settingService;
    }

    public void InitContext(RogueContext context)
    {
        Utility.LayerMapLoader layerMapLoader = InitLayerMapLoader(AssetBundleManager);
        ConfigRepository configRepository = new ConfigRepository(layerMapLoader);
        PrefabLoader prefabLoader = new PrefabLoader(AssetBundleManager);
        PlayerCache playerCache = new PlayerCache();
        PlayerFactory playerFactory = new PlayerFactory();
        PlayerProvideRegistry playerProvideRegistry = InitPlayerProviderRegistry
            (playerCache, prefabLoader, playerFactory, InputManager, context.PlayerParent);
        MapBuilder mapBuilder = InitMapBuilder(playerProvideRegistry);
        LayerProgression layerProgression = new LayerProgression();
        ContextDependencyInject(
            context,
            configRepository,
            mapBuilder,
            playerProvideRegistry,
            layerProgression,
            InputManager);
        InjectCameraSetting(context);
    }

    private void ContextDependencyInject(
        RogueContext context,
        ConfigRepository configRepository,
        MapBuilder mapBuilder,
        PlayerProvideRegistry playerProvideRegistry,
        LayerProgression layerProgression,
        InputManager inputManager)
    {
        context.BindConfigRepository(configRepository);
        context.BindMapBuilder(mapBuilder);
        context.BindPlayerRegistry(playerProvideRegistry);
        context.BindLayerProgression(layerProgression);
        context.BindInputManager(inputManager);
    }
    private Utility.LayerMapLoader InitLayerMapLoader(AssetBundleManager abManager)
    {
        return new Utility.LayerMapLoader(abManager);
    }
    private MapBuilder InitMapBuilder(PlayerProvideRegistry provideRegistry)
    {
        MapBuilder mapBuilder = new MapBuilder();
        NodeBuilder nodeBuilder = new NodeBuilder();
        EdgeBuilder edgeBuilder = new EdgeBuilder();
        StepBuilder stepBuilder = new StepBuilder();
        ComponentFactory componentFactory = new ComponentFactory();
        RegisterComponentFactory(componentFactory, provideRegistry);
        stepBuilder.DependencesInject(componentFactory);
        nodeBuilder.BindStepBuilder(stepBuilder);
        mapBuilder.BindNodeBuilder(nodeBuilder);
        mapBuilder.BindEdgeBuilder(edgeBuilder);
        return mapBuilder;
    }
    private PlayerProvideRegistry InitPlayerProviderRegistry(
        PlayerCache playerCache,
        PrefabLoader prefabLoader,
        PlayerFactory playerFactory,
        InputManager inputManager,
        Transform playerParent
        )
    {
        PlayerProvideRegistry playerProviderRegistry = new PlayerProvideRegistry();
        playerProviderRegistry.Register<DialoguePlayer>(new DialoguePlayerProvider(playerCache, prefabLoader, playerFactory, inputManager, playerParent));
        playerProviderRegistry.Register<ChoicePlayer>(new ChoicePlayerProvider(playerCache, prefabLoader, playerFactory, inputManager, playerParent));
        playerProviderRegistry.Register<CharacterPlayer>(new CharacterPlayerProvider(playerCache, prefabLoader, playerFactory, playerParent));
        playerProviderRegistry.Register<BGPlayer>(new BGPlayerProvider(playerCache, prefabLoader, playerFactory, playerParent));
        return playerProviderRegistry;
    }
    private void RegisterComponentFactory(
        ComponentFactory componentFactory,
        PlayerProvideRegistry playerProvideRegistry)
    {
        if (componentFactory == null)
        {
            Debug.LogError("RegisterComponentFactory failed: ComponentFactory is null.");
            return;
        }
        if (playerProvideRegistry == null)
        {
            Debug.LogError("RegisterComponentFactory failed: PlayerProviderRegistry is null.");
            return;
        }
        if (playerProvideRegistry.TryGet<DialoguePlayer>(out var dplayer))
        {
            componentFactory.Register<DialogueBehaviour>(
            (DialogueBehaviour) => new DialogueComponent(
                DialogueBehaviour, dplayer, AudioSystem,SettingService));
        }
        if (playerProvideRegistry.TryGet<ChoicePlayer>(out var cplayer))
        {
            componentFactory.Register<ChoiceBehaviour>(
            (ChoiceBehaviour) => new ChoiceComponent(
                ChoiceBehaviour, cplayer));
        }
        if (playerProvideRegistry.TryGet<CharacterPlayer>(out var charplayer))
        {
            componentFactory.Register<CharacterBehaviour>(
                (CharacterBehaviour) => new CharacterComponent(
                    CharacterBehaviour, charplayer));
        }
        if (playerProvideRegistry.TryGet<BGPlayer>(out var bgplayer))
        {
            componentFactory.Register<BGBehaviour>(
                (BGBehaviour) => new BGComponent(
                    BGBehaviour, bgplayer));
        }
        componentFactory.Register<BGMBehaviour>(
            (BGMBehaviour) => new BGMComponent(
                BGMBehaviour, AudioSystem));
    }
    private void InjectCameraSetting(RogueContext context) =>
            context.Camera.InitAttribute(
                context.SettingService.Config.DeveloperSetting.CameraMovingSpeed,
                context.SettingService.Config.DeveloperSetting.Offset);
}