using Setting.Config;

namespace Setting.Running
{
    public class SettingData
    {
        public Running.AudioSetting AudioSetting { get; private set; }
        public Running.DisplaySetting DisplaySetting { get; private set; }
        public Running.InputSetting InputSetting { get; private set; }
        public Running.TextSetting TextSetting { get; private set; }
        public SettingData(SettingData settingData)
        {
            AudioSetting = new AudioSetting(settingData.AudioSetting);
            DisplaySetting = new DisplaySetting(settingData.DisplaySetting);
            InputSetting = new InputSetting(settingData.InputSetting);
            TextSetting = new TextSetting(settingData.TextSetting);
        }
        public SettingData(DefaultSettingConfig config)
        {
            AudioSetting = new AudioSetting(config.AudioSetting);
            DisplaySetting = new DisplaySetting(config.DisplaySetting);
            InputSetting = new InputSetting(config.InputSetting);
            TextSetting = new TextSetting(config.TextSetting);
        }
        public void Override(Save.SavedSetting savedSetting)
        {
            AudioSetting.Override(savedSetting.AudioSetting);
            DisplaySetting.Override(savedSetting.DisplaySetting);
            InputSetting.Override(savedSetting.InputSetting);
            TextSetting.Override(savedSetting.TextSetting);
        }
    }
}