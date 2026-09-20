using UnityEngine;
using Utility;

namespace Running
{
    public class BGPlayerProvider : IPlayerProvider<BGPlayer>
    {
        private PlayerCache PlayerCache;
        private PrefabLoader PrefabLoader;
        private PlayerFactory PlayerFactory;
        private Transform PlayerParent;
        public BGPlayerProvider(PlayerCache playerCache, PrefabLoader prefabLoader, PlayerFactory playerFactory, Transform playerParent)
        {
            PlayerCache = playerCache;
            PrefabLoader = prefabLoader;
            PlayerFactory = playerFactory;
            PlayerParent = playerParent;
        }

        public BGPlayer GetPlayer()
        {
            if (PlayerCache.TryGet(out BGPlayer player))
            {
                return player;
            }
            player = GameObject.FindFirstObjectByType<BGPlayer>();
            if (player != null)
            {
                PlayerCache.Set(player);
                return player;
            }
            if (PrefabLoader.TryLoad("path", "BGPlayer", out GameObject result))
            {
                BGPlayer bgPlayer = PlayerFactory.CreatePlayer<BGPlayer>(result, PlayerParent);
                PlayerCache.Set(bgPlayer);
                return bgPlayer;
            }
            return null;
        }
    }
}