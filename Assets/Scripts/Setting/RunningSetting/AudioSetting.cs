using Config;
using UnityEngine;

namespace Running
{
    public class AudioSetting:Setting
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
        public AudioSetting(AudioSettingConfig config)
        {
            MasterVolume=config.MasterVolume;
            BgmVolume=config.BgmVolume;
            SeVolume=config.SeVolume;
            VoiceVolume = config.VoiceVolume;
            AudioPlaybackSpeed=config.AudioPlaybackSpeed;
        }
    }
}
