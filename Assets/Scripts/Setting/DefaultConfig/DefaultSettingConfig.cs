using UnityEngine;

namespace Setting.Config
{
    [CreateAssetMenu(fileName = "DefaultSettingConfig", menuName = "ConfigInfo/Setting/Default")]
    public class DefaultSettingConfig : ScriptableObject
    {
        public AudioSettingConfig AudioSetting;
        public InputSettingConfig InputSetting;
        public TextSettingConfig TextSetting;
        public DisplaySettingConfig DisplaySetting;
        public DeveloperSettings DeveloperSetting;
        public bool TryGetSetting<T>(out T setting)
      where T : Setting
        {
            if (AudioSetting is T audioSetting)
            {
                setting = audioSetting;
                return true;
            }

            if (InputSetting is T inputSetting)
            {
                setting = inputSetting;
                return true;
            }

            if (TextSetting is T textSetting)
            {
                setting = textSetting;
                return true;
            }

            if (DisplaySetting is T displaySetting)
            {
                setting = displaySetting;
                return true;
            }

            if (DeveloperSetting is T developerSetting)
            {
                setting = developerSetting;
                return true;
            }

            setting = null;
            return false;
        }
    }
}
