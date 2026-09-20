using UnityEngine;
namespace Input
{

    public class StepInputState : IInputState
    {
        public bool CanBuffer(InputCommand command)
        {
            return false;
        }
        public bool CanHandle(InputCommand command)
        {
            return command != null && command.Input is IStepClick;
        }
        public void Handle(InputCommand command)
        {
            if (command?.Input is not IStepClick stepclick)
            {
                Debug.LogError(
             "StepInputState cannot handle input because it is not IStepClick.");
                return;
            }
            stepclick.Click();
        }
    }
}