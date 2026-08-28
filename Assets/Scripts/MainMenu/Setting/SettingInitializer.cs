using Config;
using Running;
using UnityEngine;

public sealed class SettingInitializer
{
    private readonly SettingLoader Loader;

    public SettingInitializer(SettingLoader loader)
    {
        if (loader == null)
        {
            Debug.LogError(
                "SettingInitializer initialization failed: " +
                "SettingLoader is null.");
            return;
        }

        Loader = loader;
    }

    public bool TryInitSetting(out SettingRepository settingRepository)
    {
        settingRepository = null;

        if (Loader == null)
        {
            Debug.LogError(
                "Setting initialization failed: " +
                "SettingLoader is null.");
            return false;
        }

        if (!Loader.TryLoadConfig(out DefaultSettingConfig defaultSettingConfig))
        {
            return false;
        }

        if (!IsValidConfig(defaultSettingConfig))
        {
            return false;
        }

        SettingData settingData = new SettingData(defaultSettingConfig);
        settingRepository = new SettingRepository(settingData);
        return settingRepository.SettingData != null;
    }

    private bool IsValidConfig(DefaultSettingConfig defaultSettingConfig)
    {
        if (defaultSettingConfig.AudioSetting == null)
        {
            Debug.LogError("Default AudioSettingConfig is null.");
            return false;
        }

        if (defaultSettingConfig.DisplaySetting == null)
        {
            Debug.LogError("Default DisplaySettingConfig is null.");
            return false;
        }

        if (defaultSettingConfig.InputSetting == null)
        {
            Debug.LogError("Default InputSettingConfig is null.");
            return false;
        }

        if (defaultSettingConfig.TextSetting == null)
        {
            Debug.LogError("Default TextSettingConfig is null.");
            return false;
        }

        if (defaultSettingConfig.DeveloperSetting == null)
        {
            Debug.LogError("Default DeveloperSettings is null.");
            return false;
        }

        return true;
    }
}
