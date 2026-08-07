using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Running
{
    public class DialoguePlayerProvider : IPlayerProvider<DialoguePlayer>
    {
        public PlayerCache PlayerCache { get; private set; }
        public PrefabLoader PrefabLoader { get; private set; }
        public PlayerFactory PlayerFactory { get; private set; }
        public Transform PlayerParent { get; private set; }
        public DialoguePlayerProvider(PlayerCache playerCache, PrefabLoader prefabLoader, 
                                      PlayerFactory playerFactory,Transform parent)
        {
            PlayerCache = playerCache;
            PrefabLoader = prefabLoader;
            PlayerFactory = playerFactory;
            PlayerParent = parent;
        }

        public DialoguePlayer GetPlayer()
        {
            //尝试从缓存列表中查找并返回
            if(PlayerCache.TryGet<DialoguePlayer>(out var player))
            {
                return player;
            }
            //从场景中获取并注册
            player = GameObject.FindFirstObjectByType<DialoguePlayer>();
            if (player != null)
            {
                PlayerCache.Set<DialoguePlayer>(player);
                return player;
            }
            // 2. PrefabLoader 加载预制体。
            if(PrefabLoader.TryLoad("path","DialoguePlayer",out GameObject result))
            {
                // 3. PlayerFactory 实例化 DialoguePlayer。
                DialoguePlayer dialoguePlayer = PlayerFactory.CreatePlayer<DialoguePlayer>(result, PlayerParent);
                // 4. 写入缓存。
                PlayerCache.Set<DialoguePlayer>(dialoguePlayer);
                // 5. 返回 DialoguePlayer。
                return dialoguePlayer;
            }
            return null;
        }
    }
}