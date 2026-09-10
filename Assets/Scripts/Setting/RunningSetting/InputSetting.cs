using Setting.Config;
using UnityEngine;

namespace Setting.Running
{
    public class InputSetting:Setting
    {
        [Min(0f)]
        public float StepAdvanceInterval = 0.2f;
        public InputSetting(InputSettingConfig config)
        {
            StepAdvanceInterval=config.StepAdvanceInterval;
        }
        public void Override(Save.InputSetting inputSetting)
        {
            StepAdvanceInterval=inputSetting.StepAdvanceInterval;
        }
    }
}
