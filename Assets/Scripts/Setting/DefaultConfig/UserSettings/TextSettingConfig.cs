using UnityEngine;

namespace Setting.Config
{
    [CreateAssetMenu(fileName = "TextSettingConfig", menuName = "ConfigInfo/Setting/Text")]
    public class TextSettingConfig : Setting
    {
        public bool TypewriterEnabled = true;
        [Min(0f)]
        public float CharactersPerSecond = 30f;
        [Min(0f)]
        public float AutoAdvanceDelay = 1f;
        [Range(0.5f, 2f)]
        public float TextPlaybackSpeed = 1f;
    }
}
