using Running;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Running
{
    public class MapBuilder
    {
        private NodeBuilder NodeBuilder;
        public void BindNodeBuilder(NodeBuilder nodeBuilder)
        {
            this.NodeBuilder = nodeBuilder;
        }
        public Running.Map BuildMap(Config.Map configmap)
        {
            Map map = new Map();
            configmap.Nodes.ForEach(node =>
            {
                map.Nodes.Add(NodeBuilder.BuildNode(node));
            });
            return map;
        }
    }
}