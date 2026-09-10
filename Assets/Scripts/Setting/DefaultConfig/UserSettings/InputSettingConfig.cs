using UnityEngine;

namespace Setting.Config
{
    [CreateAssetMenu(fileName = "InputSettingConfig", menuName = "ConfigInfo/Setting/Input")]
    public class InputSettingConfig : Setting
    {
        [Min(0f)]
        public float StepAdvanceInterval = 0.2f;
    }
}
