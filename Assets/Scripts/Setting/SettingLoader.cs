using Config;
using UnityEngine;

namespace Setting
{
    public sealed class SettingLoader
    {
        private readonly DefaultSettingConfig DefaultSettingConfig;

        public SettingLoader(DefaultSettingConfig defaultSettingConfig)
        {
            if (defaultSettingConfig == null)
            {
                Debug.LogError(
                    "SettingLoader initialization failed: " +
                    "DefaultSettingConfig is null.");
                return;
            }

            DefaultSettingConfig = defaultSettingConfig;
        }

        public bool TryLoadConfig(out DefaultSettingConfig defaultSettingConfig)
        {
            defaultSettingConfig = DefaultSettingConfig;

            if (defaultSettingConfig == null)
            {
                Debug.LogError(
                    "SettingLoader failed to load: " +
                    "DefaultSettingConfig is null.");
                return false;
            }

            return true;
        }
    }
}
