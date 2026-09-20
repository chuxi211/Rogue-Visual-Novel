using System;
using System.Collections.Generic;
using UnityEngine;

namespace Config
{
    [Serializable]
    [CreateAssetMenu(fileName = "Map", menuName = "ConfigInfo/Rogue/Map")]
    //后续需补充ID
    //用于持久化存档
    public class Map : ScriptableObject
    {
        public List<Node> Nodes;
        public List<Edge> Edges;
    }
}
