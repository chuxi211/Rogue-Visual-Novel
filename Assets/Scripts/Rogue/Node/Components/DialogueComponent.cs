using Audio;
using Setting;
using Setting.Events;
using Setting.Running;
using UnityEditor.TextCore.Text;
using UnityEngine;

namespace Running
{
    public class DialogueComponent : IComponent
    {
        private Config.DialogueBehaviour DialogueBehaviour;
        private DialoguePlayer Player;
        private IPlayerProvider<DialoguePlayer> DialoguePlayerProvider;
        private IVoicePlayback VoicePlayback;
        private ITextReader TextReader;
        public DialogueComponent(
            Config.DialogueBehaviour dialogueBehaviour,
            IPlayerProvider<DialoguePlayer> dialoguePlayerProvider,
            IVoicePlayback voicePlayback,
            ITextReader textReader)
        {
            this.DialogueBehaviour = dialogueBehaviour;
            DialoguePlayerProvider = dialoguePlayerProvider;
            VoicePlayback = voicePlayback;
            TextReader = textReader;
            if (DialogueBehaviour == null) Debug.LogError("DialogueComponent: Behaviour is null");
            if (DialoguePlayerProvider == null) Debug.LogError("DialogueComponent:PlayerProvider is null");
            if (VoicePlayback == null) Debug.LogError("DialogueComponent:VoicePlayback is null");
            if (TextReader == null) Debug.LogError("DialogueComponent:TextReader is null");
        }
        public void Enter()
        {
            Debug.Log("DialogueComponent OnEnter");
            TextSetting textSetting=TextReader?.GetTextSetting();
            bool typingenable = textSetting.TypewriterEnabled;
            float charpersec = textSetting.CharactersPerSecond;
            Player = DialoguePlayerProvider.GetPlayer();
            Player.SetText(DialogueBehaviour.DialogueText,typingenable,(int)charpersec);
            Player.Show();
            VoicePlayback.PlayVoice(DialogueBehaviour.DialogueAudioID);
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