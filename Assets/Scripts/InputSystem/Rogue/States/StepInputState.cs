using Setting;
using UnityEngine;
namespace Input
{

    public class StepInputState : IInputState
    {
        private IInputReader InputReader;
        private float NextAllowedInputTime;
        public StepInputState(IInputReader inputReader)
        {
            InputReader = inputReader;
        }
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
            if (Time.unscaledTime < NextAllowedInputTime)
            {
                return;
            }

            Setting.Running.InputSetting setting = InputReader?.GetInputSetting();
            if (setting == null)
            {
                Debug.LogError(
                    "StepInputState failed to handle input: InputSetting is null.");
                return;
            }

            float interval = setting.StepAdvanceInterval;
            if (float.IsNaN(interval) ||
                float.IsInfinity(interval) ||
                interval < 0f)
            {
                Debug.LogError(
                    $"StepInputState received invalid StepAdvanceInterval: {interval}.");
                return;
            }

            // 在执行前更新时间，避免 Execute 过程中发生重入。
            NextAllowedInputTime = Time.unscaledTime + interval;
            stepclick.Click();
        }
    }
}