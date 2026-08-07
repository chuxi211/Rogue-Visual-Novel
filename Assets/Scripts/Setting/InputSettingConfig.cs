using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "InputSettingConfig", menuName = "ConfigInfo/Setting/Input")]
    public class InputSettingConfig : ScriptableObject
    {
        [Min(0f)]
        public float StepAdvanceInterval = 0.2f;
    }
}
