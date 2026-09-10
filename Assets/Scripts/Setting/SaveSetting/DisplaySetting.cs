using System;
using UnityEngine;

namespace Setting.Save
{
    [Serializable]
    public class DisplaySetting : Setting
    {
        [Min(1)]
        public int Width = 1920;
        [Min(1)]
        public int Height = 1080;
        public FullScreenMode FullScreenMode = FullScreenMode.FullScreenWindow;
        [Min(0)]
        public int VSyncCount = 1;
        public int TargetFrameRate = 60;
        public DisplaySetting() { }
        public DisplaySetting(Running.DisplaySetting displaySetting)
        {
            Width = displaySetting.Width;
            Height = displaySetting.Height;
            FullScreenMode = displaySetting.FullScreenMode;
            VSyncCount = displaySetting.VSyncCount;
            TargetFrameRate = displaySetting.TargetFrameRate;
        }
    }
}