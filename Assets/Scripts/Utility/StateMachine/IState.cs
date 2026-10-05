public interface IState
{
    void Enter();
    void Exit();
    void Suspend();
    void Resume();
}