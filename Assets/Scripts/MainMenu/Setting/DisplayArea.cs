using Input;
using Setting;
using Setting.Running;
using UnityEngine;

namespace MainMenu.Input
{
    public class DisplayArea : MonoBehaviour
    {
        private IDisplayEditor DisplayEditor;
        private IDisplayReader DisplayReader;

        public SettingDropdown PixelSize;
        public SettingDropdown FullScreenMode;
        public SettingSlider TargetFrameRate;

        public bool Init(
            InputManager inputManager,
            IDisplayEditor displayEditor,
            IDisplayReader displayReader)
        {
            if (inputManager == null ||
                displayEditor == null || displayReader == null)
            {
                Debug.LogError(
                    "DisplayArea initialization failed: " +
                    "one or more dependencies are null.");
                return false;
            }

            if (PixelSize==null || FullScreenMode == null ||
                TargetFrameRate == null)
            {
                Debug.LogError(
                    "DisplayArea initialization failed: " +
                    "one or more setting input references are null.");
                return false;
            }

            DisplayEditor = displayEditor;
            DisplayReader = displayReader;

            bool success = true;
            success &= PixelSize.BindInput(inputManager);
            success &= PixelSize.BindFunc(ApplyPixelSize);

            success &= FullScreenMode.BindInput(inputManager);
            success &= FullScreenMode.BindFunc(ApplyFullScreenMode);

            success &= TargetFrameRate.BindInput(inputManager);
            success &= TargetFrameRate.BindFunc(value =>
                DisplayEditor.TryUpdateDisplay(
                    setting => setting.TargetFrameRate = Mathf.RoundToInt(value)));

            return success;
        }

        public bool SetValue()
        {
            if (DisplayReader == null)
            {
                Debug.LogError(
                    "DisplayArea failed to set values: " +
                    "DisplayReader is null.");
                return false;
            }

            if (PixelSize == null || FullScreenMode == null ||
                TargetFrameRate == null)
            {
                Debug.LogError(
                    "DisplayArea failed to set values: " +
                    "one or more setting input references are null.");
                return false;
            }

            DisplaySetting setting = DisplayReader.GetDisplaySetting();
            if (setting == null)
            {
                Debug.LogError(
                    "DisplayArea failed to set values: " +
                    "DisplaySetting is null.");
                return false;
            }

            bool success = true;
            success &= PixelSize.SetValue((int)setting.PixelSize);
            success &= FullScreenMode.SetValue((int)setting.FullScreenMode);
            success &= TargetFrameRate.SetValue(setting.TargetFrameRate);
            return success;
        }

        private bool ApplyFullScreenMode(int value)
        {
            if (!System.Enum.IsDefined(
                typeof(UnityEngine.FullScreenMode), value))
            {
                Debug.LogError(
                    "DisplayArea failed to apply FullScreenMode: " +
                    $"value {value} is not defined.");
                return false;
            }

            return DisplayEditor.TryUpdateDisplay(
                setting => setting.FullScreenMode =
                    (UnityEngine.FullScreenMode)value);
        }
        private bool ApplyPixelSize(int value)
        {
            if(!System.Enum.IsDefined(
                typeof(PixelSize), value))
            {
                Debug.LogError(
                    "DisplayArea failed to apply PixelSize: " +
                    $"value {value} is not defined.");
                return false;
            }
            return DisplayEditor.TryUpdateDisplay(setting=> setting.PixelSize =(PixelSize)value);
        }
    }
}
