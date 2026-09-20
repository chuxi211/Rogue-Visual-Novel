using Input;
using UnityEngine;
using Utility;
namespace Running
{
    public class ChoicePlayerProvider : IPlayerProvider<ChoicePlayer>
    {
        public PlayerCache PlayerCache { get; private set; }
        public PrefabLoader PrefabLoader { get; private set; }
        public PlayerFactory PlayerFactory { get; private set; }
        public Transform PlayerParent { get; private set; }
        public InputManager InputManager { get; private set; }
        public ChoicePlayerProvider(
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

        public ChoicePlayer GetPlayer()
        {
            if (InputManager == null)
            {
                Debug.LogError(
                    "ChoicePlayerProvider failed: InputManager is null.");
                return null;
            }

            if (PlayerCache == null)
            {
                Debug.LogError(
                    "ChoicePlayerProvider failed: PlayerCache is null.");
                return null;
            }

            if (PlayerCache.TryGet<ChoicePlayer>(out var player))
            {
                player.BindInput(InputManager);
                return player;
            }
            player = GameObject.FindFirstObjectByType<ChoicePlayer>();
            if (player != null)
            {
                player.BindInput(InputManager);
                PlayerCache.Set<ChoicePlayer>(player);
                return player;
            }
            if (PrefabLoader == null)
            {
                Debug.LogError(
                    "ChoicePlayerProvider failed: PrefabLoader is null.");
                return null;
            }

            if (PrefabLoader.TryLoad("path", "ChoicePlayer", out GameObject result))
            {
                if (PlayerFactory == null)
                {
                    Debug.LogError(
                        "ChoicePlayerProvider failed: PlayerFactory is null.");
                    return null;
                }

                if (PlayerParent == null)
                {
                    Debug.LogError(
                        "ChoicePlayerProvider failed: PlayerParent is null.");
                    return null;
                }

                ChoicePlayer choicePlayer = PlayerFactory.CreatePlayer<ChoicePlayer>(result, PlayerParent);
                if (choicePlayer == null)
                {
                    Debug.LogError(
                        "ChoicePlayerProvider failed to create ChoicePlayer.");
                    return null;
                }

                choicePlayer.BindInput(InputManager);
                PlayerCache.Set<ChoicePlayer>(choicePlayer);
                return choicePlayer;
            }
            return null;
        }
    }
}
