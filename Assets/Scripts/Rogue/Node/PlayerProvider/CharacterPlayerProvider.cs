using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Running
{
    public class CharacterPlayerProvider : IPlayerProvider<CharacterPlayer>
    {
        private PlayerCache PlayerCache;
        private PlayerFactory PlayerFactory;
        private PrefabLoader PrefabLoader;
        public CharacterPlayerProvider(PlayerCache playerCache,  PrefabLoader prefabLoader,PlayerFactory playerFactory)
        {
            PlayerCache = playerCache;
            PlayerFactory = playerFactory;
            PrefabLoader = prefabLoader;
        }

        public CharacterPlayer GetPlayer()
        {
            throw new System.NotImplementedException();
        }
    }
}