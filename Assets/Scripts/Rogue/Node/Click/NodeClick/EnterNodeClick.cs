using UnityEngine;
namespace Input
{
    public class EnterNodeClick : MonoBehaviour, INodeClick
    {
        private NodeActor Actor;
        public void BindActor(NodeActor nodeActor)
        {
            Actor = nodeActor;
            if (Actor == null)
            {
                Debug.LogError("EnterNodeClick BindActor Actor is null");
            }
        }
        public void Click()
        {
            EventBus.Publish(new ERequestEnterNode(Actor));
        }

        public void Execute()
        {
            Click();
        }
    }
}