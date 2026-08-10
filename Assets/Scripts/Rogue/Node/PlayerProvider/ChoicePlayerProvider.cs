using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
namespace Running
{
    public class ChoicePlayerProvider:IPlayerProvider<ChoicePlayer>
    {
        public PlayerCache PlayerCache { get; private set; }
        public PrefabLoader PrefabLoader { get; private set; }
        public PlayerFactory PlayerFactory { get; private set; }
        public Transform PlayerParent { get; private set; }
        public ChoicePlayerProvider(PlayerCache playerCache, PrefabLoader prefabLoader,
                                      PlayerFactory playerFactory, Transform parent)
        {
            PlayerCache = playerCache;
            PrefabLoader = prefabLoader;
            PlayerFactory = playerFactory;
            PlayerParent = parent;
        }

        public ChoicePlayer GetPlayer()
        {
            if (PlayerCache.TryGet<ChoicePlayer>(out var player))
            {
                return player;
            }
            player = GameObject.FindFirstObjectByType<ChoicePlayer>();
            if(player != null)
            {
                PlayerCache.Set<ChoicePlayer>(player);
                return player;
            }
            if(PrefabLoader.TryLoad("path", "ChoicePlayer", out GameObject result))
            {
                ChoicePlayer choicePlayer = PlayerFactory.CreatePlayer<ChoicePlayer>(result, PlayerParent);
                PlayerCache.Set<ChoicePlayer>(choicePlayer);
                return choicePlayer;
            }
            return null;
        }
    }
}