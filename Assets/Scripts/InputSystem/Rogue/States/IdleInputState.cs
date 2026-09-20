using UnityEngine;
namespace Input
{
    //InputStateMachine Init 时的默认状态
    //后续需根据业务切换
    public class IdleInputState : IInputState
    {
        public bool CanBuffer(InputCommand command)
        {
            return false;
        }

        public bool CanHandle(InputCommand command)
        {
            return false;
        }

        public void Handle(InputCommand command)
        {
            Debug.Log("DefaultInputState: Handle command");
        }
    }
}