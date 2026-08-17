using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Running
{
    public class EdgeBuilder
    {
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
    }
}