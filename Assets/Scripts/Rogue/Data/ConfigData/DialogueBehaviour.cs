using Audio;
using System.Collections;
using System.Collections.Generic;
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
