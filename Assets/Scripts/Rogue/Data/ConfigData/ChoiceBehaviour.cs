using System;
using System.Collections.Generic;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "ChoiceBehaviour", menuName = "ConfigInfo/Rogue/NodeBehaviours/ChoiceBehaviour")]
    public class ChoiceBehaviour : NodeBehaviour
    {
        public List<ChoiceInfo> Choices;
    }
    [Serializable]
    public class ChoiceInfo
    {
        public string Text;
        public StepID NextID;
    }
}
