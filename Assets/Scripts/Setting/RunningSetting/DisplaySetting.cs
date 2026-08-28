using Config;
using UnityEngine;

namespace Running
{
    public class DisplaySetting
    {
        [Min(1)]
        public int DefaultWidth = 1920;
        [Min(1)]
        public int DefaultHeight = 1080;
        public FullScreenMode DefaultFullScreenMode = FullScreenMode.FullScreenWindow;
        [Min(0)]
        public int DefaultVSyncCount = 1;
        public int DefaultTargetFrameRate = 60;
        public DisplaySetting(DisplaySettingConfig config)
        {
            DefaultWidth = config.DefaultWidth;
            DefaultHeight = config.DefaultHeight;
            DefaultFullScreenMode = config.DefaultFullScreenMode;
            DefaultVSyncCount =config.DefaultVSyncCount;
            DefaultTargetFrameRate = config.DefaultTargetFrameRate;
        }
    }
}
