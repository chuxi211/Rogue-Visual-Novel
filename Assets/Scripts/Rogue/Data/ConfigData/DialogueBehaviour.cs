using Audio;
using UnityEngine;
namespace Config
{
    [CreateAssetMenu(fileName = "DialogueBehaviour", menuName = "ConfigInfo/Rogue/NodeBehaviours/DialogueBehaviour")]
    public class DialogueBehaviour : NodeBehaviour
    {
        public string DialogueText;
        public AudioID DialogueAudioID;
    }
}
