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
        public CharacterComponent(CharacterBehaviour behaviour, IPlayerProvider<CharacterPlayer> playerProvider)
        {
            Behaviour = behaviour;
            PlayerProvider = playerProvider;
        }

        public void Enter(Node node)
        {
            throw new System.NotImplementedException();
        }

        public void Exit(Node node)
        {
            throw new System.NotImplementedException();
        }
    }
}