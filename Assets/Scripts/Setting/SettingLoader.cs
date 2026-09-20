using Setting.Config;
using Setting.Save;
using System.IO;
using System.Text;
using UnityEngine;


namespace Setting
{

    //该Loader不仅负责UserSetting加载
    //还负责默认Setting加载
    //因此它会在CoreBootstrap中实例化并注入依赖
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
        //Deserialize SavedSetting object from Jsom text
        public bool TryLoadSaved(out SavedSetting savedSetting)
        {
            savedSetting = null;
            string userSetting = Path.Combine(Application.persistentDataPath,
                DefaultSettingConfig.DeveloperSetting.UserSettingDire,
                DefaultSettingConfig.DeveloperSetting.UserSettingsFileName);
            //UserSetting不存在不一定是错误
            //不存在时立刻返回
            if (!File.Exists(userSetting))
            {
                Debug.Log("The File UserSetting is not found");
                return true;
            }
            //若存在则加载并反序列化
            string json = File.ReadAllText(userSetting, Encoding.UTF8);
            savedSetting = JsonUtility.FromJson<SavedSetting>(json);
            return savedSetting != null;
        }
        public bool TryLoadConfig(out DefaultSettingConfig defaultSettingConfig)
        {
            defaultSettingConfig = DefaultSettingConfig;

            if (!IsValidConfig(defaultSettingConfig))
            {
                return false;
            }
            return true;
        }

        private bool IsValidConfig(DefaultSettingConfig defaultSettingConfig)
        {
            if (defaultSettingConfig == null)
            {
                Debug.LogError(
                    "SettingLoader failed to load: " +
                    "DefaultSettingConfig is null.");
                return false;
            }
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
}
