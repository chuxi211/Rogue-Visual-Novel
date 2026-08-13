using Config;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Running
{
    public class CharacterComponent : IComponent
    {
        public CharacterBehaviour Behaviour { get; private set; }
        public IPlayerProvider<CharacterPlayer> PlayerProvider { get; private set; }
        private CharacterPlayer Player;
        private CharacterPlaybackHandle PlaybackHandle;
        public CharacterComponent(CharacterBehaviour behaviour, IPlayerProvider<CharacterPlayer> playerProvider)
        {
            Behaviour = behaviour;
            PlayerProvider = playerProvider;
        }

        public void Enter(Node node)
        {
            Player = PlayerProvider.GetPlayer();
            if (Player == null)
            {
                Debug.LogError(
                    "CharacterComponent: Player is null.");
                return;
            }

            if (Behaviour == null)
            {
                Debug.LogError(
                    "CharacterComponent: Behaviour is null.");
                return;
            }

            if (Behaviour.Character == null)
            {
                Debug.LogError(
                    "CharacterComponent: Character is null.");
                return;
            }
            Sprite visual = Behaviour.Character.GetIllustration(Behaviour.VisualState);
            if(!Player.TryShow(visual, Behaviour.NormalizedPosition, out PlaybackHandle))
            {
                Debug.LogError(
                    "CharacterComponent: Failed to show character.");
                return;
            }
        }

        public void Exit(Node node)
        {
            throw new System.NotImplementedException();
        }
    }
}