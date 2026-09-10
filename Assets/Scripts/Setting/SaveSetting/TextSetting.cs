using System;
using UnityEngine;

namespace Setting.Save
{
    [Serializable]
    public class TextSetting : Setting
    {
        public bool TypewriterEnabled = true;
        [Min(0f)]
        public float CharactersPerSecond = 30f;
        [Min(0f)]
        public float AutoAdvanceDelay = 1f;
        [Range(0.5f, 2f)]
        public float TextPlaybackSpeed = 1f;

        public TextSetting()
        {
        }
        public TextSetting(Running.TextSetting textSetting)
        {
            TypewriterEnabled = textSetting.TypewriterEnabled;
            CharactersPerSecond = textSetting.CharactersPerSecond;
            AutoAdvanceDelay = textSetting.AutoAdvanceDelay;
            TextPlaybackSpeed = textSetting.TextPlaybackSpeed;
        }
    }
}