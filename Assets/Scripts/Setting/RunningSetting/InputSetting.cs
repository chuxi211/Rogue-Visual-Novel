using Config;
using UnityEngine;

namespace Running
{
    public class InputSetting
    {
        [Min(0f)]
        public float StepAdvanceInterval = 0.2f;
        public InputSetting(InputSettingConfig config)
        {
            StepAdvanceInterval=config.StepAdvanceInterval;
        }
    }
}
