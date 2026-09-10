using UnityEngine;

namespace Setting.Save
{
    [ System.Serializable]
    public class InputSetting : Setting
    {
        [Min(0f)]
        public float StepAdvanceInterval = 0.2f;
        public InputSetting() { }
        public InputSetting(Running.InputSetting inputSetting)
        {
            StepAdvanceInterval=inputSetting.StepAdvanceInterval;
        }
    }
}