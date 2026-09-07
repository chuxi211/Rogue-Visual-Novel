using Config;
using Running;
using StateMachine.Rogue;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Utility;

public class RogueInitiallizer
{
    private readonly AssetBundleManager AssetBundleManager;
    public RogueInitiallizer(AssetBundleManager assetBundleManager)
    {
        AssetBundleManager = assetBundleManager;
    }

    public void InitContext(RogueContext context)
    {
        Utility.LayerMapLoader layerMapLoader = InitLayerMapLoader(AssetBundleManager);
        ConfigRepository configRepository = new ConfigRepository(layerMapLoader);
        PrefabLoader prefabLoader = new PrefabLoader(AssetBundleManager);
        PlayerCache playerCache = new PlayerCache();
        PlayerFactory playerFactory = new PlayerFactory();
        PlayerProvideRegistry playerProvideRegistry = InitPlayerProviderRegistry
            (playerCache, prefabLoader, playerFactory, context.PlayerParent);
        MapBuilder mapBuilder = InitMapBuilder(playerProvideRegistry);
        LayerProgression layerProgression = new LayerProgression();
        ContextDependencyInject(context, configRepository, mapBuilder, playerProvideRegistry, layerProgression);
        InjectCameraSetting(context);
    }

    private void ContextDependencyInject(
        RogueContext context,
        ConfigRepository configRepository,
        MapBuilder mapBuilder,
        PlayerProvideRegistry playerProvideRegistry, LayerProgression layerProgression)
    {
        context.BindConfigRepository(configRepository);
        context.BindMapBuilder(mapBuilder);
        context.BindPlayerRegistry(playerProvideRegistry);
        context.BindLayerProgression(layerProgression);
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
        stepBuilder.BindComponentFactory(componentFactory);
        nodeBuilder.BindStepBuilder(stepBuilder);
        mapBuilder.BindNodeBuilder(nodeBuilder);
        mapBuilder.BindEdgeBuilder(edgeBuilder);
        return mapBuilder;
    }
    private PlayerProvideRegistry InitPlayerProviderRegistry(
        PlayerCache playerCache,
        PrefabLoader prefabLoader,
        PlayerFactory playerFactory,
        Transform playerParent
        )
    {
        PlayerProvideRegistry playerProviderRegistry = new PlayerProvideRegistry();
        playerProviderRegistry.Register<DialoguePlayer>(new DialoguePlayerProvider(playerCache, prefabLoader, playerFactory, playerParent));
        playerProviderRegistry.Register<ChoicePlayer>(new ChoicePlayerProvider(playerCache, prefabLoader, playerFactory, playerParent));
        playerProviderRegistry.Register<CharacterPlayer>(new CharacterPlayerProvider(playerCache, prefabLoader, playerFactory, playerParent));
        playerProviderRegistry.Register<BGPlayer>(new BGPlayerProvider(playerCache, prefabLoader, playerFactory, playerParent));
        return playerProviderRegistry;
    }
    private void RegisterComponentFactory(
        ComponentFactory componentFactory,
        PlayerProvideRegistry playerProvideRegistry)
    {
        //Component的实例化需要Provider提供provider<>
        //provider又不可能凭空变出来
        //只能从PlayerProvideRegistry中获取
        //获取到之后再注册到componentFactory中
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
            DialogueBehaviour => new DialogueComponent(
                DialogueBehaviour, dplayer));
        }
        if (playerProvideRegistry.TryGet<ChoicePlayer>(out var cplayer))
        {
            componentFactory.Register<ChoiceBehaviour>(
            ChoiceBehaviour => new ChoiceComponent(
                ChoiceBehaviour, cplayer));
        }
        if (playerProvideRegistry.TryGet<CharacterPlayer>(out var charplayer))
        {
            componentFactory.Register<CharacterBehaviour>(
                CharacterBehaviour => new CharacterComponent(
                    CharacterBehaviour, charplayer));
        }
        if (playerProvideRegistry.TryGet<BGPlayer>(out var bgplayer))
        {
            componentFactory.Register<BGBehaviour>(
                BGBehaviour => new BGComponent(
                    BGBehaviour, bgplayer));
        }
    }
    private void InjectCameraSetting(RogueContext context) =>
            context.Camera.InitAttribute(
                context.SettingRepository.SettingData.OtherSetting.CameraMovingSpeed,
                context.SettingRepository.SettingData.OtherSetting.Offset);
}