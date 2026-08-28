using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Running
{
    public class EdgeBuilder
    {
        //Edge暂时没有GO的视觉表现，暂时不修改参数
        public Edge BuildEdge(Config.Edge configEdge,Dictionary<Config.Node,Running.Node> nodeDict)
        {
            if (nodeDict.TryGetValue(configEdge.From, out Node fromNode) && nodeDict.TryGetValue(configEdge.To, out Node toNode))
            {
                return new Edge(fromNode, toNode);
            }
            else
            {
                Debug.LogError($"Failed to build edge: From {configEdge.From} or To {configEdge.To} not found in node dictionary.");
                return null;
            }
        }
        public void ClearEdge(Running.Edge edge)
        {

        }
    }
}