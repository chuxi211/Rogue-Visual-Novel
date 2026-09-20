using System;
using UnityEngine;

namespace Setting.Save
{
    [Serializable]
    public class DisplaySetting : Setting
    {
        public PixelSize PixelSize;
        public FullScreenMode FullScreenMode = FullScreenMode.FullScreenWindow;
        public int TargetFrameRate = 60;
        public DisplaySetting() { }
        public DisplaySetting(Running.DisplaySetting displaySetting)
        {
            PixelSize = displaySetting.PixelSize;
            FullScreenMode = displaySetting.FullScreenMode;
            TargetFrameRate = displaySetting.TargetFrameRate;
        }

    }
}