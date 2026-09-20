using UnityEngine;
namespace Running
{
    public class NodeBuilder
    {
        private StepBuilder StepBuilder;
        public void BindStepBuilder(StepBuilder stepBuilder)
        {
            this.StepBuilder = stepBuilder;
            if (StepBuilder == null)
            {
                Debug.LogError("StepBuilder is null");
            }
        }
        public Node BuildNode(Config.Node configNode, Transform parent, Vector2 nodeSpacing)
        {
            if (configNode == null)
            {
                Debug.LogError("ConfigNode is null");
                return null;
            }
            Node node = new Node(configNode.Position, configNode.AdvanceType);
            Vector3 position = new Vector3(configNode.Position.X * nodeSpacing.x, configNode.Position.Y * nodeSpacing.y);
            GameObject go = GameObject.Instantiate(configNode.NodePrefab, position, Quaternion.identity, parent);
            NodeActor nodeActor = go.GetComponent<NodeActor>();

            if (nodeActor == null)
            {
                Debug.LogError("NodeBuilder: NodeActor is null.");
                return null;
            }

            nodeActor.BindNode(node);
            configNode.Steps.ForEach(configStep =>
            {
                Step step = StepBuilder.BuildStep(configStep);
                if (step != null)
                {
                    node.AddStep(step);
                }
                else
                {
                    Debug.LogError($"NodeBuilder: Failed to build step for config {configStep.GetType().Name}");
                }
            });
            Debug.Log($"NodeBuilder: Built node with {node.Steps.Count} steps.");
            return node;
        }
        public void ClearNode(Running.Node node)
        {

        }
    }
}