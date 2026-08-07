using Running;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
namespace StateMachine.Rogue
{
    public class RogueContext
    {
        public PlayerProvideRegistry PlayerProvideRegistry { get; private set; }
        public ConfigRepository ConfigRepository {  get; private set; }
        public MapBuilder MapBuilder { get; private set; }
        public PlayerCache PlayerCache { get; private set; }
        public PrefabLoader PrefabLoader { get; private set; }
        public PlayerFactory PlayerFactory { get; private set; }
        public Transform PlayerParent { get; private set; }
        public void BindConfigRepository(ConfigRepository configRepository)
        {
            this.ConfigRepository = configRepository;
            if(ConfigRepository == null)
            {
                Debug.LogError("ConfigRepository is null");
            }
        }
        public void BindMapBuilder(MapBuilder mapBuilder)
        {
            this.MapBuilder = mapBuilder;
            if(MapBuilder == null)
            {
                Debug.LogError("MapBuilder is null");
            }
        }
        public void BindPlayerRegistry(PlayerProvideRegistry playerProvideRegistry)
        {
            this.PlayerProvideRegistry = playerProvideRegistry;
            if(PlayerProvideRegistry == null)
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
    }
}