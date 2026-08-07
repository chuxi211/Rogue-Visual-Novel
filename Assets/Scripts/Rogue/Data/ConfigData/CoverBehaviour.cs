using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Config
{
    [Serializable]
    [CreateAssetMenu(fileName ="NodeCover",menuName ="ConfigInfo/Rogue/NodeBehaviours/Cover")]
    public class CoverBehaviour : NodeBehaviour
    {
        public Sprite Cover;
    }
}
