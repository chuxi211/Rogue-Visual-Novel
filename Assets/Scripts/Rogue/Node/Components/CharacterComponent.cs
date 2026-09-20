using Config;
using UnityEngine;

namespace Running
{
    public class CharacterComponent : IComponent
    {
        public CharacterBehaviour Behaviour { get; private set; }
        public IPlayerProvider<CharacterPlayer> PlayerProvider { get; private set; }
        private CharacterPlayer Player;
        public CharacterComponent(CharacterBehaviour behaviour, IPlayerProvider<CharacterPlayer> playerProvider)
        {
            Behaviour = behaviour;
            PlayerProvider = playerProvider;
        }

        public void Enter()
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
            Player.SetCharacter(Behaviour.Character.ID, visual, Behaviour.NormalizedPosition);
        }

        public void ExitOnStepEnd()
        {

        }

        public void ExitOnNodeEnd()
        {
            Player.ClearAll();
            Player.HideAll();
        }
    }
}