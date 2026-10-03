using Input;
using Setting;
using Setting.Running;
using UnityEngine;

namespace MainMenu.Input
{
    public class InputArea : MonoBehaviour
    {
        private IInputEditor InputEditor;
        private IInputReader InputReader;

        public SettingSlider StepAdvanceInterval;

        public bool Init(
            InputManager inputManager,
            IInputEditor inputEditor,
            IInputReader inputReader)
        {
            if (inputManager == null || inputEditor == null || inputReader == null)
            {
                Debug.LogError(
                    "InputArea initialization failed: " +
                    "one or more dependencies are null.");
                return false;
            }

            if (StepAdvanceInterval == null)
            {
                Debug.LogError(
                    "InputArea initialization failed: " +
                    "StepAdvanceInterval is null.");
                return false;
            }

            InputEditor = inputEditor;
            InputReader = inputReader;

            return StepAdvanceInterval.BindInput(inputManager) &
                StepAdvanceInterval.BindFunc(value =>
                    InputEditor.TryUpdateInput(
                        setting => setting.StepAdvanceInterval = value));
        }

        public bool SetValue()
        {
            if (InputReader == null)
            {
                Debug.LogError(
                    "InputArea failed to set values: " +
                    "InputReader is null.");
                return false;
            }

            if (StepAdvanceInterval == null)
            {
                Debug.LogError(
                    "InputArea failed to set values: " +
                    "StepAdvanceInterval is null.");
                return false;
            }

            InputSetting setting = InputReader.GetInputSetting();
            if (setting == null)
            {
                Debug.LogError(
                    "InputArea failed to set values: " +
                    "InputSetting is null.");
                return false;
            }

            return StepAdvanceInterval.SetValue(
                setting.StepAdvanceInterval);
        }
    }
}
