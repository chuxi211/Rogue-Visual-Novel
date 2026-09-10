using Config;
using Setting.Config;
using Setting.Running;
using Setting;
using UnityEngine;
using Setting.Save;
using System.IO;

public sealed class SettingInitializer
{
    private readonly SettingLoader Loader;
    private SettingSerializer Serializer;

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
    public bool TryInitSetting(out SettingService settingService)
    {
        settingService = null;
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
        if (!TryInitSettingRepository(defaultSettingConfig,out SettingRepository repository))
        {
            return false;
        }
        if (!TryInitSettingSerializer(defaultSettingConfig))
        {
            return false;
        }
        settingService=new SettingService(repository,defaultSettingConfig,Loader,Serializer);
        return true;
    }

    public bool TryInitSettingRepository(DefaultSettingConfig defaultSettingConfig, out SettingRepository settingRepository)
    {
        settingRepository = null;


        if(!Loader.TryLoadSaved(out SavedSetting savedSetting))
        {
            return false;
        }

        SettingData settingData = new SettingData(defaultSettingConfig);
        settingRepository = new SettingRepository(settingData);
        //不为空则覆盖默认设置
        if (savedSetting != null)
        {
            settingRepository.Override(savedSetting);
        }
        return settingRepository.SettingData != null;
    }
    private bool TryInitSettingSerializer(DefaultSettingConfig defaultSettingConfig)
    {

        string settingDire = defaultSettingConfig.DeveloperSetting.UserSettingDire;
        string settingName=defaultSettingConfig.DeveloperSetting.UserSettingsFileName;

        Serializer =new SettingSerializer(settingDire,settingName);
        return true;
    }
}
