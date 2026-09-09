using Audio;
using System;
using UnityEngine;

namespace Config
{
    //与BG的生命周期类似
    //OnNodeEnd统一清理
    //而非OnStepEnd
    [Serializable]
    [CreateAssetMenu(fileName ="BGMBhaviour",menuName = "ConfigInfo/Rogue/NodeBehaviours/BGMBehaviour")]
    public class BGMBehaviour : NodeBehaviour
    {
        public AudioID AudioID = new AudioID();
    }
}