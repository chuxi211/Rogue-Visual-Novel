using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "BGBehaviour",menuName = "ConfigInfo/Rogue/NodeBehaviours/BGBehaviour")]
    public sealed class BGBehaviour:NodeBehaviour
    {
        public Sprite BG;
    }
}