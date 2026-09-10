using UnityEngine;

namespace Setting.Config
{
    [CreateAssetMenu(fileName = "AudioSettingConfig", menuName = "ConfigInfo/Setting/Audio")]
    public class AudioSettingConfig : Setting
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
    }
}
