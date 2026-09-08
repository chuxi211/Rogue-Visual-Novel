using Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Running
{
    public class DialogueComponent : IComponent
    {
        private Config.DialogueBehaviour DialogueBehaviour;
        private DialoguePlayer Player;
        private IPlayerProvider<DialoguePlayer> DialoguePlayerProvider;
        private IVoicePlayback VoicePlayback;
        public DialogueComponent(Config.DialogueBehaviour dialogueBehaviour, IPlayerProvider<DialoguePlayer> dialoguePlayerProvider,IVoicePlayback voicePlayback)
        {
            this.DialogueBehaviour = dialogueBehaviour;
            DialoguePlayerProvider = dialoguePlayerProvider;
            VoicePlayback = voicePlayback;
            if (DialogueBehaviour == null) Debug.LogError("DialogueComponent: Behaviour is null");
            if (DialoguePlayerProvider == null) Debug.LogError("DialogueComponent:PlayerProvider is null");
            if (VoicePlayback == null) Debug.LogError("DialogueComponent:VoicePlayback is null");
        }
        public void Enter()
        {
            Debug.Log("DialogueComponent OnEnter");
            Player=DialoguePlayerProvider.GetPlayer();
            Player.SetText(DialogueBehaviour.DialogueText);
            Player.Show();
        }

        public void ExitOnNodeEnd()
        {
            
        }

        public void ExitOnStepEnd()
        {
            Debug.Log("DialogueComponent OnExit");
            Player.ClearText();
            Player.Hide();
        }
    }
}