using Setting.Config;
using Setting.Events;
using Setting.Running;
using Setting.Save;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Setting
{
    public class SettingService
    {
        public SettingRepository Repository { get;private set; }
        public DefaultSettingConfig Config { get; private set; }
        public SettingLoader SettingLoader { get; private set; }
        public SettingSerializer SettingSerializer { get; private set; }
        public SettingService(SettingRepository repository,DefaultSettingConfig config, SettingLoader settingLoader, SettingSerializer settingSerializer)
        {
            Repository = repository;
            Config = config;
            SettingLoader = settingLoader;
            SettingSerializer = settingSerializer;
        }
        public bool GetConfigSetting<T>(out T setting) where T : Config.Setting
        {
            setting = default;
            if (Config == null)
            {
                Debug.LogError(
              "SettingService failed to get config setting: " +
              "DefaultSettingConfig is null.");
                return false;
            }
            if (!Config.TryGetSetting(out setting))
            {
                Debug.LogError(
                    "SettingService failed to get config setting: " +
                    $"Setting of type '{typeof(T).Name}' was not found.");
                return false;
            }

            return true;
        }
        private void OnExit()
        {
            //EventBus.Unsubscript<ERequestChangeMasterAudioVolume>();
        }
    }
}