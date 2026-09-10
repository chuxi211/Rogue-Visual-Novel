using System;
using UnityEngine;

namespace Setting.Save
{
    [Serializable]
    public class AudioSetting: Setting
    {
        [Range(0f, 1f)]
        public float MasterVolume = 1f;
        [Range(0f, 1f)]
        public float BgmVolume = 1f;
        [Range(0f, 1f)]
        public float SeVolume = 1f;
        [Range(0f, 1f)]
        public float VoiceVolume = 1f;
        [Range(0.5f, 2f)]
        public float AudioPlaybackSpeed = 1f;
        public AudioSetting() { }
        public AudioSetting(Running.AudioSetting audioSetting)
        {
            MasterVolume = audioSetting.MasterVolume;
            BgmVolume = audioSetting.BgmVolume;
            SeVolume = audioSetting.SeVolume;
            VoiceVolume = audioSetting.VoiceVolume;
            AudioPlaybackSpeed = audioSetting.AudioPlaybackSpeed;
        }
    }
}