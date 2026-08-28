using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "DefaultSettingConfig", menuName = "ConfigInfo/Setting/Default")]
    public class DefaultSettingConfig : ScriptableObject
    {
        public AudioSettingConfig AudioSetting;
        public InputSettingConfig InputSetting;
        public TextSettingConfig TextSetting;
        public DisplaySettingConfig DisplaySetting;
        public DeveloperSettings DeveloperSetting;
    }
}
