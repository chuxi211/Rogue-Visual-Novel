using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "Step", menuName = "ConfigInfo/Rogue/Step", order = 1)]
    [System.Serializable]
    public class Step:ScriptableObject
    {
        public StepID ID;
        public List<NodeBehaviour> Behaviours;
    }
}
