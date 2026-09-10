using Setting.Config;
using Setting.Running;
using Setting.Save;
using System.IO;
using System.Text;
using UnityEngine;

namespace Setting
{
    public class SettingSerializer
    {
        private string UserSettingDire;
        private string UserSettingName;
        public SettingSerializer(string userSettingDire, string userSettingName)
        {
            UserSettingDire = userSettingDire;
            UserSettingName = userSettingName;
        }

        //Transfor SettingData to SavedSetting and
        //Serailize to Json
        public void SerializeSetting(SettingData settingData)
        {
            string jsonPath = Path.Combine(Application.persistentDataPath, UserSettingDire,UserSettingName);
            string jsonDire=Path.GetDirectoryName(jsonPath);
            if (!Directory.Exists(jsonDire))
            {
                Directory.CreateDirectory(jsonDire);
            }
            SavedSetting savedSetting = new SavedSetting(settingData);
            string jsonFile=JsonUtility.ToJson(savedSetting);
            File.WriteAllText(jsonPath, jsonFile, Encoding.UTF8);
        }
    }
}