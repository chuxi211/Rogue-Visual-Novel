using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Config
{
    [Serializable]
    [CreateAssetMenu(fileName ="Map",menuName ="ConfigInfo/Rogue/Map")]
    public class Map : ScriptableObject
    {
        public List<Node> Nodes;
        public List<Edge> Edges;
    }
}
