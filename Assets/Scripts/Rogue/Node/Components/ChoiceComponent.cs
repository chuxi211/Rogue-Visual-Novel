using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Running
{
    public class ChoiceComponent : IComponent
    {
        private Config.ChoiceBehaviour ChoiceBehaviour;
        private ChoicePlayer Player;
        //为了防止各个Component在各自逻辑中直接查找Player
        //通过IPlayerProvider来获取Player，避免Component之间的耦合
        private IPlayerProvider<ChoicePlayer> ChoicePlayerProvider;
        public ChoiceComponent(Config.ChoiceBehaviour choiceBehaviour, IPlayerProvider<ChoicePlayer> choicePlayerProvider)
        {
            this.ChoiceBehaviour = choiceBehaviour;
            ChoicePlayerProvider = choicePlayerProvider;
            if (ChoiceBehaviour == null) Debug.LogError("ChoiceComponent: Behaviour is null");
            if (ChoicePlayerProvider == null) Debug.LogError("ChoiceComponent: PlayerProvider is null");
        }
        public void Enter(Node node)
        {
            Debug.Log("ChoiceComponent OnEnter");
            Player = ChoicePlayerProvider.GetPlayer();
            Player.SetChoicesAndShow(ChoiceBehaviour.Choices);
        }
        public void Exit(Node node)
        {
            Debug.Log("ChoiceComponent OnExit");
            Player.ClearAllChoicesText();
            Player.Hide();
            Player = null;
        }
    }
}