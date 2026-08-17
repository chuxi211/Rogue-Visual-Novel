using Running;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Running
{
    public class MapBuilder
    {
        private NodeBuilder NodeBuilder;
        private EdgeBuilder EdgeBuilder;
        public void BindNodeBuilder(NodeBuilder nodeBuilder)
        {
            this.NodeBuilder = nodeBuilder;
        }
        public void BindEdgeBuilder(EdgeBuilder edgeBuilder)
        {
            this.EdgeBuilder = edgeBuilder;
        }
        public Running.Map BuildMap(Config.Map configmap)
        {
            Map map = new Map();
            Dictionary<Config.Node, Running.Node> NodeDict = new Dictionary<Config.Node, Running.Node>();
            configmap.Nodes.ForEach(node =>
            {
                Node runnode = NodeBuilder.BuildNode(node);
                if (runnode != null)
                {
                    map.Nodes.Add(runnode);
                    NodeDict.Add(node, runnode);
                }
            });
            configmap.Edges.ForEach(edge =>
            {
                if(edge != null)
                map.TryAddEdge(EdgeBuilder.BuildEdge(edge,NodeDict));
            });
            return map;
        }
    }
}