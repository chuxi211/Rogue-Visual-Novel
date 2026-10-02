using Setting;
using Setting.Events;
using Setting.Running;
using UnityEngine;

namespace Display
{
    public class DisplaySystem
    {
        public DisplaySystem(DisplaySetting setting)
        {
            ApplySetting(setting);
            EventBus.Subscript<ESettingChanged>(OnSettingChanged);
        }
        public void Dispose()
        {
            EventBus.Unsubscript<ESettingChanged>(OnSettingChanged);
        }
        private void OnSettingChanged(ESettingChanged eSetting)
        {
            if ((eSetting.Scope & SettingChangeScope.Display) == 0)
            {
                return;
            }
            ApplySetting(eSetting.SettingData.DisplaySetting);
        }
        private void ApplySetting(DisplaySetting setting)
        {
            if(!TryGetResolution(setting.PixelSize,out int width,out int height))
            {
                return;
            }
            Screen.SetResolution(width,height,setting.FullScreenMode);
            Application.targetFrameRate = setting.TargetFrameRate;
        }
        private bool TryGetResolution(PixelSize pixelSize,out int width,out int height)
        {
            switch (pixelSize)
            {
                case PixelSize.Res1920x1080:
                    width = 1920;
                    height = 1080;
                    return true;

                case PixelSize.Res2560x1080:
                    width = 2560;
                    height = 1080;
                    return true;

                case PixelSize.Res2560x1440:
                    width = 2560;
                    height = 1440;
                    return true;

                default:
                    width = 0;
                    height = 0;
                    Debug.LogError(
                        $"Unsupported PixelSize: {pixelSize}.");
                    return false;
            }
        }
    }
}