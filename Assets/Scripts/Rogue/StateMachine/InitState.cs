using Config;
using Running;
using StateMachine.Node;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Utility;

namespace StateMachine.Rogue
{
    public class InitState : RogueState
    {
        private RogueInitiallizer RogueInitiallizer;
        public InitState(RogueStateMachine stateMachine) : base(stateMachine)
        {
        }
        public override void Enter()
        {
            base.Enter();
            Init();
        }
        public override void Exit()
        {
            base.Exit();
        }
        private void Init()
        {
            Utility.LayerMapLoader layerMapLoader= InitLayerMapLoader();
            RogueInitiallizer = new RogueInitiallizer(layerMapLoader);
            ConfigRepository configRepository = new ConfigRepository(RogueInitiallizer.LayerMapLoader);
            PlayerCache playerCache = new PlayerCache();
            PrefabLoader prefabLoader = new PrefabLoader();
            PlayerFactory playerFactory = new PlayerFactory();
            PlayerProvideRegistry playerProvideRegistry = InitPlayerProviderRegistry(playerCache,prefabLoader,playerFactory,StateMachine.Context.PlayerParent);
            MapBuilder mapBuilder= InitMapBuilder(playerProvideRegistry);
            ContextDependencyInject(configRepository,mapBuilder,playerProvideRegistry);
            NewGame();
        }
        private void ContextDependencyInject(
            ConfigRepository configRepository,
            MapBuilder mapBuilder,
            PlayerProvideRegistry playerProvideRegistry)
        {
            StateMachine.Context.BindConfigRepository(configRepository);
            StateMachine.Context.BindMapBuilder(mapBuilder);
            StateMachine.Context.BindPlayerRegistry(playerProvideRegistry);
        }
        private Utility.LayerMapLoader InitLayerMapLoader()
        {
            string bundlePath = Path.Combine(Application.streamingAssetsPath, "AssetBundles/Windows/LayerMaps");
            string manifestBundlePath = Path.Combine(bundlePath, "LayerMaps");

            AssetBundle manifestBundle = AssetBundle.LoadFromFile(manifestBundlePath);

            if (manifestBundle == null)
            {
                Debug.LogError($"Failed to load manifest bundle: {manifestBundlePath}");
                return null;
            }

            AssetBundleManifest manifest = manifestBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");

            if (manifest == null)
            {
                Debug.LogError("Failed to load AssetBundleManifest.");
                return null; 
            }
            return new Utility.LayerMapLoader(bundlePath, manifest);
        }
        private MapBuilder InitMapBuilder(PlayerProvideRegistry provideRegistry)
        {
            MapBuilder mapBuilder = new MapBuilder();
            NodeBuilder nodeBuilder = new NodeBuilder();
            StepBuilder stepBuilder = new StepBuilder();
            ComponentFactory componentFactory = new ComponentFactory();
            NodeStateFactory stateFactory = new NodeStateFactory();
            RegisterComponentFactory(componentFactory,provideRegistry);
            stepBuilder.BindComponentFactory(componentFactory);
            nodeBuilder.BindStepBuilder(stepBuilder);
            mapBuilder.BindNodeBuilder(nodeBuilder);
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
            playerProviderRegistry.Register<DialoguePlayer>(new DialoguePlayerProvider(playerCache,prefabLoader,playerFactory,playerParent));
            return playerProviderRegistry;
        }
        private void RegisterComponentFactory(ComponentFactory componentFactory,PlayerProvideRegistry playerProvideRegistry)
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
            if(playerProvideRegistry.TryGet<DialoguePlayer>(out var player))
            {
                componentFactory.Register<DialogueBehaviour>(
                DialogueBehaviour => new DialogueComponent(
                    DialogueBehaviour,player));
            }
        }
        private void NewGame()
        {
            StateMachine.Context.ConfigRepository.TryGetLayerMap(1, out LayerMapGroup layerMapGroup);
            StateMachine.PushState(new BuildMapState(StateMachine, layerMapGroup.Variants[0]));
        }
        private void Load()
        {

        }
    }
}
