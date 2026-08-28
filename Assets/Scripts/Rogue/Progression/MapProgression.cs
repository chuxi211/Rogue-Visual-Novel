using Config;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Running
{
    public class MapProgression
    {
        public Map Map { get; private set; }
        public Node CurrentNode { get;private set; }
        public MapProgression(Map map)
        {
            this.Map = map;
        }
        public bool TryMoveTo(Node node)
        {
            if(node==null) return false;
            if (!Map.Nodes.Contains(node)) return false;
            if(node.IsCompleted) return false;
            if (CurrentNode == null)
            {
                if (node.AdvanceType != NodeAdvanceType.Start)
                {
                    return false;
                }

                CurrentNode = node;
                return true;
            }
            foreach (var edge in CurrentNode.GetOutgoingEdges())
            {
                if (edge.To == node)
                {
                    CurrentNode = node; 
                    return true;
                }
            }
            
            Debug.LogError("The node is not Reachable");
            return false;
        }
        public bool IsMapCompleted(Node node)
        {
            return node != null && node.IsCompleted && node.AdvanceType == Config.NodeAdvanceType.End;
        }
    }
}