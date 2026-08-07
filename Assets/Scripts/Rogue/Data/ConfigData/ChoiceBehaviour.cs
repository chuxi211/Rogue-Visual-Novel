using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "ChoiceBehaviour", menuName = "ConfigInfo/Rogue/NodeBehaviours/ChoiceBehaviour")]
    public class ChoiceBehaviour : NodeBehaviour
    {
        public List<string> ChoiceTextList;
    }
}
