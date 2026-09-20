using System;
using UnityEngine;

namespace Setting.Save
{
    [Serializable]
    public class SavedSetting
    {
        public string Version = Application.version;
        public AudioSetting AudioSetting = new AudioSetting();
        public InputSetting InputSetting = new InputSetting();
        public DisplaySetting DisplaySetting = new DisplaySetting();
        public TextSetting TextSetting = new TextSetting();
        public SavedSetting() { }
        public SavedSetting(Running.SettingData settingData)
        {
            AudioSetting = new AudioSetting(settingData.AudioSetting);
            InputSetting = new InputSetting(settingData.InputSetting);
            DisplaySetting = new DisplaySetting(settingData.DisplaySetting);
            TextSetting = new TextSetting(settingData.TextSetting);
        }
    }
}