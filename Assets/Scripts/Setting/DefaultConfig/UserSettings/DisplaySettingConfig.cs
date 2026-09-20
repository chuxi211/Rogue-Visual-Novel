using UnityEngine;

namespace Setting.Config
{
    [CreateAssetMenu(fileName = "DisplaySettingConfig", menuName = "ConfigInfo/Setting/Display")]
    public class DisplaySettingConfig : Setting
    {
        public PixelSize PixelSize;
        public FullScreenMode DefaultFullScreenMode = FullScreenMode.FullScreenWindow;
        public int DefaultTargetFrameRate = 60;
    }
}
