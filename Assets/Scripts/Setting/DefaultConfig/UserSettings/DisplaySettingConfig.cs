using UnityEngine;

namespace Setting.Config
{
    [CreateAssetMenu(fileName = "DisplaySettingConfig", menuName = "ConfigInfo/Setting/Display")]
    public class DisplaySettingConfig : Setting
    {
        [Min(1)]
        public int DefaultWidth = 1920;
        [Min(1)]
        public int DefaultHeight = 1080;
        public FullScreenMode DefaultFullScreenMode = FullScreenMode.FullScreenWindow;
        [Min(0)]
        public int DefaultVSyncCount = 1;
        public int DefaultTargetFrameRate = 60;
    }
}
