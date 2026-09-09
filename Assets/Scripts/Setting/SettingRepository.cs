using UnityEngine;
using Running;

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
    }
}
