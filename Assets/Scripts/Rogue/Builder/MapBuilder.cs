using System.Collections.Generic;
using UnityEngine;
namespace Running
{
    //我不认为在这里销毁所有Map相关的GameObject是一件多明智的事情
    //但是，为了销毁GameObject再独立出一个类也不见得是多么必要
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
        public Running.Map BuildMap(Config.Map configmap, Transform parent, Vector2 nodeSpacing)
        {
            Map map = new Map(configmap.ID);
            Dictionary<Config.Node, Running.Node> NodeDict = new Dictionary<Config.Node, Running.Node>();
            configmap.Nodes.ForEach(node =>
            {
                Node runnode = NodeBuilder.BuildNode(node, parent, nodeSpacing);
                if (runnode != null)
                {
                    map.Nodes.Add(runnode);
                    NodeDict.Add(node, runnode);
                }
            });
            configmap.Edges.ForEach(edge =>
            {
                if (edge != null)
                    map.TryAddEdge(EdgeBuilder.BuildEdge(edge, NodeDict));
            });
            parent.gameObject.SetActive(true);
            return map;
        }
        public void ClearMap(Transform mapparent)
        {
            mapparent.gameObject.SetActive(false);
            foreach (Transform child in mapparent)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }
}