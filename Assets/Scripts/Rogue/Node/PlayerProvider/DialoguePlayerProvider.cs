using Input;
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
        public InputManager InputManager { get; private set; }
        public Transform PlayerParent { get; private set; }
        public DialoguePlayerProvider(
            PlayerCache playerCache,
            PrefabLoader prefabLoader,
            PlayerFactory playerFactory,
            InputManager inputManager,
            Transform parent)
        {
            PlayerCache = playerCache;
            PrefabLoader = prefabLoader;
            PlayerFactory = playerFactory;
            InputManager = inputManager;
            PlayerParent = parent;
        }

        public DialoguePlayer GetPlayer()
        {
            if (InputManager == null)
            {
                Debug.LogError(
                    "DialoguePlayerProvider failed: InputManager is null.");
                return null;
            }

            if (PlayerCache == null)
            {
                Debug.LogError(
                    "DialoguePlayerProvider failed: PlayerCache is null.");
                return null;
            }

            //尝试从缓存列表中查找并返回
            if(PlayerCache.TryGet<DialoguePlayer>(out var player))
            {
                player.BindInput(InputManager);
                return player;
            }
            //从场景中获取并注册
            player = GameObject.FindFirstObjectByType<DialoguePlayer>();
            if (player != null)
            {
                player.BindInput(InputManager);
                PlayerCache.Set<DialoguePlayer>(player);
                return player;
            }
            // 2. PrefabLoader 加载预制体。
            if (PrefabLoader == null)
            {
                Debug.LogError(
                    "DialoguePlayerProvider failed: PrefabLoader is null.");
                return null;
            }

            if(PrefabLoader.TryLoad("path","DialoguePlayer",out GameObject result))
            {
                if (PlayerFactory == null)
                {
                    Debug.LogError(
                        "DialoguePlayerProvider failed: PlayerFactory is null.");
                    return null;
                }

                if (PlayerParent == null)
                {
                    Debug.LogError(
                        "DialoguePlayerProvider failed: PlayerParent is null.");
                    return null;
                }

                // 3. PlayerFactory 实例化 DialoguePlayer。
                DialoguePlayer dialoguePlayer = PlayerFactory.CreatePlayer<DialoguePlayer>(result, PlayerParent);
                if (dialoguePlayer == null)
                {
                    Debug.LogError(
                        "DialoguePlayerProvider failed to create DialoguePlayer.");
                    return null;
                }

                // 4. 写入缓存。
                dialoguePlayer.BindInput(InputManager);
                PlayerCache.Set<DialoguePlayer>(dialoguePlayer);
                // 5. 返回 DialoguePlayer。
                return dialoguePlayer;
            }
            return null;
        }
    }
}
