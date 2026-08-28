using Config;
using UnityEngine;

namespace Running
{
    public class TextSetting
    {
        public bool TypewriterEnabled = true;
        [Min(0f)]
        public float CharactersPerSecond = 30f;
        [Min(0f)]
        public float AutoAdvanceDelay = 1f;
        [Range(0.5f, 2f)]
        public float TextPlaybackSpeed = 1f;
        public TextSetting(TextSettingConfig config)
        {
            TypewriterEnabled = config.TypewriterEnabled;
            CharactersPerSecond = config.CharactersPerSecond;
            AutoAdvanceDelay = config.AutoAdvanceDelay;
            TextPlaybackSpeed = config.TextPlaybackSpeed;
        }
    }
}
