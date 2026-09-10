using UnityEngine;
using Setting.Running;
using Setting.Save;
namespace Setting
{
    public class SettingRepository
    {
        public SettingData SettingData { get; private set; }
        public SettingRepository(SettingData settingData)
        {
            if (settingData == null)
            {
                Debug.LogError(
                    "SettingRepository initialization failed: " +
                    "SettingData is null.");
                return;
            }

            SettingData = settingData;
        }
        public void Override(SavedSetting savedSetting)
        {
            SettingData.Override(savedSetting);
        }
    }
}
