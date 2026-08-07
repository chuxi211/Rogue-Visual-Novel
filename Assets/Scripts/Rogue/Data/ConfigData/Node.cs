using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Config
{
    [CreateAssetMenu(fileName = "New Node", menuName = "ConfigInfo/Rogue/NodeInfo")]
    [Serializable]
    public class Node : ScriptableObject
    {
        public string NodeID;
        public string NodeName;
        public int Column;
        public int Row;
        public GameObject NodePrefab;
        public string NodeDescription;
        public NodeType NodeType;
        public List<Step> Steps;
        [NonSerialized]
        public List<Edge> Edges;
    }
}
