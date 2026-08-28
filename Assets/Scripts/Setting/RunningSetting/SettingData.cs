using Config;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Running
{
    public class SettingData
    {
        public Running.AudioSetting AudioSetting { get; private set; }
        public Running.DisplaySetting DisplaySetting { get; private set; }
        public Running.InputSetting InputSetting { get;private set; }
        public Running.TextSetting TextSetting { get;private set; }
        public Running.OtherSetting OtherSetting { get; private set; }
        public SettingData(DefaultSettingConfig config)
        {
            AudioSetting=new AudioSetting(config.AudioSetting);
            DisplaySetting=new DisplaySetting(config.DisplaySetting);
            InputSetting=new InputSetting(config.InputSetting);
            TextSetting=new TextSetting(config.TextSetting);
            OtherSetting=new OtherSetting(config.DeveloperSetting);
        }
    }
}