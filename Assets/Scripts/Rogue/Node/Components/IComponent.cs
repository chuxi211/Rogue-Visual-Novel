namespace Running
{
    public interface IComponent
    {
        void Enter();
        void ExitOnStepEnd();
        void ExitOnNodeEnd();
    }
}