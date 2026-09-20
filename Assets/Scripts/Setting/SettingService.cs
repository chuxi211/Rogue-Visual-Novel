using Setting.Config;
using Setting.Events;
using Setting.Running;
using System;
using UnityEngine;

namespace Setting
{
    public class SettingService : ISettingEditor,IAudioEditor,ITextEditor,IDisplayEditor,IInputEditor,
                                                 IAudioReader, ITextReader, IDisplayReader, IInputReader
    {
        public SettingRepository Repository { get; private set; }
        public DefaultSettingConfig Config { get; private set; }
        public SettingLoader SettingLoader { get; private set; }
        public SettingSerializer SettingSerializer { get; private set; }

        public SettingService(
            SettingRepository repository,
            DefaultSettingConfig config,
            SettingLoader settingLoader,
            SettingSerializer settingSerializer)
        {
            Repository = repository;
            Config = config;
            SettingLoader = settingLoader;
            SettingSerializer = settingSerializer;
        }
        public void OnExit()
        {

        }
        //Reference when Open SettingPanel
        public void BeginUpdate()
        {
            if (!TryGetRepository(out SettingRepository repository))
            {
                return;
            }

            repository.BeginUpdate();
        }

        public bool TryUpdateAudio(Action<AudioSetting> update)
        {
            return TryGetRepository(out SettingRepository repository)
                && repository.TryUpdateAudio(update);
        }

        public bool TryUpdateDisplay(Action<DisplaySetting> update)
        {
            return TryGetRepository(out SettingRepository repository)
                && repository.TryUpdateDisplay(update);
        }

        public bool TryUpdateInput(Action<InputSetting> update)
        {
            return TryGetRepository(out SettingRepository repository)
                && repository.TryUpdateInput(update);
        }

        public bool TryUpdateText(Action<TextSetting> update)
        {
            return TryGetRepository(out SettingRepository repository)
                && repository.TryUpdateText(update);
        }

        public void ConfirmUpdate()
        {
            if (!TryGetRepository(out SettingRepository repository) ||
                !TryGetSerializer(out SettingSerializer serializer))
            {
                return;
            }

            if (!repository.TryConfirmUpdate(out SettingChangeScope changeScope))
            {
                return;
            }

            if (!serializer.TrySerializeSetting(repository.SettingData))
            {
                repository.UndoUpdate();
                return;
            }

            repository.ClearPreviousData();

            EventBus.Publish(
                new ESettingChanged(repository.SettingData, changeScope));
        }

        //Reference when CanacelUpdate or Close SettingPanel
        public void CancelUpdate()
        {
            if (!TryGetRepository(out SettingRepository repository))
            {
                return;
            }

            repository.CancelUpdate();
        }

        public bool GetConfigSetting<T>(out T setting) where T : Config.Setting
        {
            setting = default;
            if (Config == null)
            {
                Debug.LogError(
                    "SettingService failed to get config setting: " +
                    "DefaultSettingConfig is null.");
                return false;
            }

            if (!Config.TryGetSetting(out setting))
            {
                Debug.LogError(
                    "SettingService failed to get config setting: " +
                    $"Setting of type '{typeof(T).Name}' was not found.");
                return false;
            }

            return true;
        }

        private bool TryGetSerializer(out SettingSerializer serializer)
        {
            serializer = SettingSerializer;
            if (serializer != null)
            {
                return true;
            }

            Debug.LogError("SettingSerializer is null");
            return false;
        }

        private bool TryGetRepository(out SettingRepository repository)
        {
            repository = Repository;
            if (repository != null)
            {
                return true;
            }

            Debug.LogError(
                "SettingService failed to edit settings: " +
                "SettingRepository is null.");
            return false;
        }

        public AudioSetting GetAudioSetting()
        {
            if (!TryGetRepository(out SettingRepository repository) ||
                repository.SettingData?.AudioSetting == null)
            {
                Debug.LogError(
                    "SettingService failed to read AudioSetting: " +
                    "AudioSetting is null.");
                return null;
            }

            return new AudioSetting(repository.SettingData.AudioSetting);
        }

        public TextSetting GetTextSetting()
        {
            if (!TryGetRepository(out SettingRepository repository) ||
                repository.SettingData?.TextSetting == null)
            {
                Debug.LogError(
                    "SettingService failed to read TextSetting: " +
                    "TextSetting is null.");
                return null;
            }

            return new TextSetting(repository.SettingData.TextSetting);
        }

        public DisplaySetting GetDisplaySetting()
        {
            if (!TryGetRepository(out SettingRepository repository) ||
                repository.SettingData?.DisplaySetting == null)
            {
                Debug.LogError(
                    "SettingService failed to read DisplaySetting: " +
                    "DisplaySetting is null.");
                return null;
            }

            return new DisplaySetting(repository.SettingData.DisplaySetting);
        }

        public InputSetting GetInputSetting()
        {
            if (!TryGetRepository(out SettingRepository repository) ||
                repository.SettingData?.InputSetting == null)
            {
                Debug.LogError(
                    "SettingService failed to read InputSetting: " +
                    "InputSetting is null.");
                return null;
            }

            return new InputSetting(repository.SettingData.InputSetting);
        }
    }
}
