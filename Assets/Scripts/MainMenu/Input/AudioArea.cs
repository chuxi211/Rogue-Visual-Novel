using Input;
using Setting;
using Setting.Running;
using UnityEngine;

namespace MainMenu.Input
{
    public class AudioArea : MonoBehaviour
    {
        private IAudioEditor AudioEditor;
        private IAudioReader AudioReader;

        public SettingSlider MasterVolume;
        public SettingSlider BGMVolume;
        public SettingSlider SEVolume;
        public SettingSlider VoiceVolume;

        public bool SetValue()
        {
            if (AudioReader == null)
            {
                Debug.LogError(
                    "AudioArea failed to set values: " +
                    "AudioReader is null.");
                return false;
            }

            if (MasterVolume == null || BGMVolume == null ||
                SEVolume == null || VoiceVolume == null)
            {
                Debug.LogError(
                    "AudioArea failed to set values: " +
                    "one or more SettingSlider references are null.");
                return false;
            }

            AudioSetting audioSetting = AudioReader.GetAudioSetting();
            if (audioSetting == null)
            {
                Debug.LogError(
                    "AudioArea failed to set values: " +
                    "AudioSetting is null.");
                return false;
            }

            bool success = true;
            success &= MasterVolume.SetValue(audioSetting.MasterVolume);
            success &= BGMVolume.SetValue(audioSetting.BgmVolume);
            success &= SEVolume.SetValue(audioSetting.SeVolume);
            success &= VoiceVolume.SetValue(audioSetting.VoiceVolume);
            return success;
        }

        public bool Init(
            InputManager inputManager,
            IAudioEditor audioEditor,
            IAudioReader audioReader)
        {
            if (inputManager == null || audioEditor == null || audioReader == null)
            {
                Debug.LogError(
                    "AudioArea initialization failed: " +
                    "one or more dependencies are null.");
                return false;
            }

            if (MasterVolume == null || BGMVolume == null ||
                SEVolume == null || VoiceVolume == null)
            {
                Debug.LogError(
                    "AudioArea initialization failed: " +
                    "one or more SettingSlider references are null.");
                return false;
            }

            AudioEditor = audioEditor;
            AudioReader = audioReader;

            bool success = true;
            success &= MasterVolume.BindInput(inputManager);
            success &= MasterVolume.BindFunc(value =>
                AudioEditor.TryUpdateAudio(
                    audioSetting => audioSetting.MasterVolume = value));

            success &= BGMVolume.BindInput(inputManager);
            success &= BGMVolume.BindFunc(value =>
                AudioEditor.TryUpdateAudio(
                    audioSetting => audioSetting.BgmVolume = value));

            success &= SEVolume.BindInput(inputManager);
            success &= SEVolume.BindFunc(value =>
                AudioEditor.TryUpdateAudio(
                    audioSetting => audioSetting.SeVolume = value));

            success &= VoiceVolume.BindInput(inputManager);
            success &= VoiceVolume.BindFunc(value =>
                AudioEditor.TryUpdateAudio(
                    audioSetting => audioSetting.VoiceVolume = value));

            return success;
        }
    }
}
