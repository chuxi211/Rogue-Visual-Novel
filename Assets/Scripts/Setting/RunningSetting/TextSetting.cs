using Setting.Config;
using UnityEngine;

namespace Setting.Running
{
    public class TextSetting : Setting
    {
        public bool TypewriterEnabled = true;
        [Min(0f)]
        public float CharactersPerSecond = 30f;
        [Min(0f)]
        public float AutoAdvanceDelay = 1f;
        [Range(0.5f, 2f)]
        public float TextPlaybackSpeed = 1f;
        public TextSetting(TextSetting textSetting)
        {
            TypewriterEnabled = textSetting.TypewriterEnabled;
            CharactersPerSecond = textSetting.CharactersPerSecond;
            AutoAdvanceDelay = textSetting.AutoAdvanceDelay;
            TextPlaybackSpeed = textSetting.TextPlaybackSpeed;
        }
        public TextSetting(TextSettingConfig config)
        {
            TypewriterEnabled = config.TypewriterEnabled;
            CharactersPerSecond = config.CharactersPerSecond;
            AutoAdvanceDelay = config.AutoAdvanceDelay;
            TextPlaybackSpeed = config.TextPlaybackSpeed;
        }
        public void Override(Save.TextSetting textSetting)
        {
            TypewriterEnabled = textSetting.TypewriterEnabled;
            CharactersPerSecond = textSetting.CharactersPerSecond;
            AutoAdvanceDelay = textSetting.AutoAdvanceDelay;
            TextPlaybackSpeed = textSetting.TextPlaybackSpeed;
        }
    }
}
