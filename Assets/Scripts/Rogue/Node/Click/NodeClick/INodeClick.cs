using Rogue.Input;

namespace Input
{
    public interface INodeClick :IRogueInput,IClick
    {
        void BindActor(NodeActor nodeActor);
    }
}