using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName ="Edge",menuName ="ConfigInfo/Rogue/Edge")]
    [Serializable]
    public class Edge:ScriptableObject
    {
        public Node From;
        public Node To;
    }
}
