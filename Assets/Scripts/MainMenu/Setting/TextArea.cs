using Input;
using Setting;
using Setting.Running;
using UnityEngine;

namespace MainMenu.Input
{
    public class TextArea : MonoBehaviour
    {
        private ITextEditor TextEditor;
        private ITextReader TextReader;

        public SettingToggle TypewriterEnabled;
        public SettingSlider CharactersPerSecond;
        public SettingSlider AutoAdvanceDelay;
        public SettingSlider TextPlaybackSpeed;

        public bool Init(
            InputManager inputManager,
            ITextEditor textEditor,
            ITextReader textReader)
        {
            if (inputManager == null || textEditor == null || textReader == null)
            {
                Debug.LogError(
                    "TextArea initialization failed: " +
                    "one or more dependencies are null.");
                return false;
            }

            if (TypewriterEnabled == null || CharactersPerSecond == null ||
                AutoAdvanceDelay == null || TextPlaybackSpeed == null)
            {
                Debug.LogError(
                    "TextArea initialization failed: " +
                    "one or more setting input references are null.");
                return false;
            }

            TextEditor = textEditor;
            TextReader = textReader;

            bool success = true;
            success &= TypewriterEnabled.BindInput(inputManager);
            success &= TypewriterEnabled.BindFunc(value =>
                TextEditor.TryUpdateText(
                    setting => setting.TypewriterEnabled = value));

            success &= CharactersPerSecond.BindInput(inputManager);
            success &= CharactersPerSecond.BindFunc(value =>
                TextEditor.TryUpdateText(
                    setting => setting.CharactersPerSecond = value));

            success &= AutoAdvanceDelay.BindInput(inputManager);
            success &= AutoAdvanceDelay.BindFunc(value =>
                TextEditor.TryUpdateText(
                    setting => setting.AutoAdvanceDelay = value));

            success &= TextPlaybackSpeed.BindInput(inputManager);
            success &= TextPlaybackSpeed.BindFunc(value =>
                TextEditor.TryUpdateText(
                    setting => setting.TextPlaybackSpeed = value));

            return success;
        }

        public bool SetValue()
        {
            if (TextReader == null)
            {
                Debug.LogError(
                    "TextArea failed to set values: " +
                    "TextReader is null.");
                return false;
            }

            if (TypewriterEnabled == null || CharactersPerSecond == null ||
                AutoAdvanceDelay == null || TextPlaybackSpeed == null)
            {
                Debug.LogError(
                    "TextArea failed to set values: " +
                    "one or more setting input references are null.");
                return false;
            }

            TextSetting setting = TextReader.GetTextSetting();
            if (setting == null)
            {
                Debug.LogError(
                    "TextArea failed to set values: " +
                    "TextSetting is null.");
                return false;
            }

            bool success = true;
            success &= TypewriterEnabled.SetValue(setting.TypewriterEnabled);
            success &= CharactersPerSecond.SetValue(setting.CharactersPerSecond);
            success &= AutoAdvanceDelay.SetValue(setting.AutoAdvanceDelay);
            success &= TextPlaybackSpeed.SetValue(setting.TextPlaybackSpeed);
            return success;
        }
    }
}
