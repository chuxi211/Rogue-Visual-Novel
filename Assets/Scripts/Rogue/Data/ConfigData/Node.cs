using System;
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
        public NodePosition Position;
        public GameObject NodePrefab;
        public string NodeDescription;
        public NodeUtilityType UtilityType;
        public NodeAdvanceType AdvanceType;
        public List<Step> Steps;
    }
    [Serializable]
    public struct NodePosition
    {
        public int X;
        public int Y;
    }
}
