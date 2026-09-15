using Setting.Config;
using UnityEngine;

namespace Setting.Running
{
    public class DisplaySetting:Setting
    {
        [Min(1)]
        public int Width = 1920;
        [Min(1)]
        public int Height = 1080;
        public FullScreenMode FullScreenMode = FullScreenMode.FullScreenWindow;
        [Min(0)]
        public int VSyncCount = 1;
        public int TargetFrameRate = 60;
        public DisplaySetting(DisplaySetting displaySetting)
        {
            Width = displaySetting.Width;
            Height = displaySetting.Height;
            FullScreenMode = displaySetting.FullScreenMode;
            VSyncCount = displaySetting.VSyncCount;
            TargetFrameRate = displaySetting.TargetFrameRate;
        }
        public DisplaySetting(DisplaySettingConfig config)
        {
            Width = config.DefaultWidth;
            Height = config.DefaultHeight;
            FullScreenMode = config.DefaultFullScreenMode;
            VSyncCount =config.DefaultVSyncCount;
            TargetFrameRate = config.DefaultTargetFrameRate;
        }
        public void Override(Save.DisplaySetting displaySetting)
        {
            Width=displaySetting.Width;
            Height=displaySetting.Height;
            FullScreenMode = displaySetting.FullScreenMode;
            VSyncCount=displaySetting.VSyncCount;
            TargetFrameRate = displaySetting.TargetFrameRate;
        }
    }
}
