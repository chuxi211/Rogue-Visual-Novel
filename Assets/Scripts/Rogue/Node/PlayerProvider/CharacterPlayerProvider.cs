using UnityEngine;
using Utility;

namespace Running
{
    public class CharacterPlayerProvider : IPlayerProvider<CharacterPlayer>
    {
        private PlayerCache PlayerCache;
        private PlayerFactory PlayerFactory;
        private PrefabLoader PrefabLoader;
        private Transform PlayerParent;
        public CharacterPlayerProvider(PlayerCache playerCache, PrefabLoader prefabLoader, PlayerFactory playerFactory, Transform parent)
        {
            PlayerCache = playerCache;
            PlayerFactory = playerFactory;
            PrefabLoader = prefabLoader;
            PlayerParent = parent;
        }

        public CharacterPlayer GetPlayer()
        {
            if (PlayerCache.TryGet(out CharacterPlayer player))
            {
                return player;
            }
            player = GameObject.FindFirstObjectByType<CharacterPlayer>();
            if (player != null)
            {
                PlayerCache.Set(player);
                return player;
            }
            if (PrefabLoader.TryLoad("path", "CharacterPlayer", out GameObject result))
            {
                CharacterPlayer characterPlayer = PlayerFactory.CreatePlayer<CharacterPlayer>(result, PlayerParent);
                PlayerCache.Set(characterPlayer);
                return characterPlayer;
            }
            return null;
        }
    }
}