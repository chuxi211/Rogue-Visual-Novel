using Setting.Config;
using UnityEngine;

namespace Setting.Running
{
    public class DisplaySetting : Setting
    {
        public PixelSize PixelSize;
        public FullScreenMode FullScreenMode = FullScreenMode.FullScreenWindow;
        public int TargetFrameRate = 60;
        public DisplaySetting(DisplaySetting displaySetting)
        {
            PixelSize=displaySetting.PixelSize;
            FullScreenMode = displaySetting.FullScreenMode;
            TargetFrameRate = displaySetting.TargetFrameRate;
        }
        public DisplaySetting(DisplaySettingConfig config)
        {
            PixelSize = config.PixelSize;
            FullScreenMode = config.DefaultFullScreenMode;
            TargetFrameRate = config.DefaultTargetFrameRate;
        }
        public void Override(Save.DisplaySetting displaySetting)
        {
            PixelSize = displaySetting.PixelSize;
            FullScreenMode = displaySetting.FullScreenMode;
            TargetFrameRate = displaySetting.TargetFrameRate;
        }
    }
}
