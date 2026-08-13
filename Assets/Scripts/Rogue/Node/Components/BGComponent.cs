using Config;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Running
{
    public class BGComponent : IComponent
    {
        private BGBehaviour BGBehaviour;
        private IPlayerProvider<BGPlayer> PlayerProvider;
        private BGPlayer Player;
        public BGComponent(BGBehaviour bGBehaviour, IPlayerProvider<BGPlayer> playerProvider)
        {
            BGBehaviour = bGBehaviour;
            PlayerProvider = playerProvider;
        }

        public void Enter(Node node)
        {
            Player= PlayerProvider.GetPlayer();
            Player.SetBG(BGBehaviour.BG);
            Player.Show();
        }

        public void Exit(Node node)
        {
            throw new System.NotImplementedException();
        }
    }
}