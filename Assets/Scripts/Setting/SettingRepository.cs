using Setting.Events;
using Setting.Running;
using System;
using UnityEngine;
using SavedSetting = Setting.Save.SavedSetting;

namespace Setting
{
    public class SettingRepository
    {
        public SettingData PreviousData { get; private set; }
        public SettingData SettingData { get; private set; }
        public bool IsUpdating => Snapshot != null;

        private SettingData Snapshot;

        public SettingRepository(SettingData settingData)
        {
            if (settingData == null)
            {
                Debug.LogError(
                    "SettingRepository initialization failed: " +
                    "SettingData is null.");
                return;
            }

            SettingData = settingData;
        }

        public void Override(SavedSetting savedSetting)
        {
            if (SettingData == null)
            {
                Debug.LogError(
                    "SettingRepository failed to override settings: " +
                    "SettingData is null.");
                return;
            }

            if (savedSetting == null)
            {
                Debug.LogError(
                    "SettingRepository failed to override settings: " +
                    "SavedSetting is null.");
                return;
            }

            if (savedSetting.AudioSetting == null
                || savedSetting.InputSetting == null
                || savedSetting.TextSetting == null
                || savedSetting.DisplaySetting == null)
            {
                Debug.LogError(
                    "SettingRepository failed to override settings: " +
                    "SavedSetting contains a null setting section.");
                return;
            }

            SettingData snapshot = new SettingData(SettingData);
            snapshot.Override(savedSetting);
            if (!(IsValid(snapshot.AudioSetting)
                && IsValid(snapshot.InputSetting)
                && IsValid(snapshot.TextSetting)
                && IsValid(snapshot.DisplaySetting)))
            {
                Debug.LogWarning(
                    "SettingRepository rejected invalid values from SavedSetting.");
                return;
            }

            SettingData = snapshot;
        }

        public void BeginUpdate()
        {
            if (SettingData == null)
            {
                Debug.LogError(
                    "SettingRepository failed to begin update: " +
                    "SettingData is null.");
                return;
            }

            if (Snapshot != null)
            {
                Debug.LogWarning(
                    "SettingRepository ignored BeginUpdate: " +
                    "an update is already in progress.");
                return;
            }

            Snapshot = new SettingData(SettingData);
        }

        public bool TryUpdateAudio(Action<AudioSetting> update)
        {
            return TryUpdate(
                data => data.AudioSetting,
                update,
                IsValid,
                nameof(AudioSetting));
        }

        public bool TryUpdateDisplay(Action<DisplaySetting> update)
        {
            return TryUpdate(
                data => data.DisplaySetting,
                update,
                IsValid,
                nameof(DisplaySetting));
        }

        public bool TryUpdateInput(Action<InputSetting> update)
        {
            return TryUpdate(
                data => data.InputSetting,
                update,
                IsValid,
                nameof(InputSetting));
        }

        public bool TryUpdateText(Action<TextSetting> update)
        {
            return TryUpdate(
                data => data.TextSetting,
                update,
                IsValid,
                nameof(TextSetting));
        }

        public bool TryConfirmUpdate(out SettingChangeScope changeScope)
        {
            changeScope = SettingChangeScope.None;

            if (Snapshot == null)
            {
                Debug.LogWarning(
                    "SettingRepository failed to confirm update: " +
                    "no update is in progress.");
                return false;
            }

            changeScope = GetChangeScope(SettingData, Snapshot);
            PreviousData = SettingData;
            SettingData = Snapshot;
            Snapshot = null;
            return true;
        }

        public void ClearPreviousData()
        {
            PreviousData = null;
        }

        public void UndoUpdate()
        {
            if (PreviousData != null)
            {
                SettingData = PreviousData;
                PreviousData = null;
            }
        }

        public void CancelUpdate()
        {
            Snapshot = null;
        }

        private bool TryUpdate<TSetting>(
            Func<SettingData, TSetting> selector,
            Action<TSetting> update,
            Func<TSetting, bool> validator,
            string settingName)
        {
            //Func<SettingData, TSetting> selector 参数类型为SettingData，返回TSetting的delegate selector
            //Action<TSetting> update 参数类型为TSetting，无返回的delegate update
            if (Snapshot == null)
            {
                Debug.LogError(
                    $"SettingRepository failed to update {settingName}: " +
                    "BeginUpdate must be called first.");
                return false;
            }

            if (update == null)
            {
                Debug.LogError(
                    $"SettingRepository failed to update {settingName}: " +
                    "update action is null.");
                return false;
            }

            // 修改候选副本，保证委托抛出异常或验证失败时不会污染当前 Snapshot。
            SettingData candidate = new SettingData(Snapshot);
            TSetting setting = selector(candidate);

            try
            {
                update(setting);
            }
            catch (Exception exception)
            {
                Debug.LogError(
                    $"SettingRepository failed to update {settingName}: " +
                    "update action threw an exception.");
                Debug.LogException(exception);
                return false;
            }

            if (!validator(setting))
            {
                Debug.LogWarning(
                    $"SettingRepository rejected invalid {settingName} values.");
                return false;
            }

            // 不保留传给外部委托的对象，避免委托缓存引用后绕过后续验证。
            Snapshot = new SettingData(candidate);
            return true;
        }

        private static bool IsValid(AudioSetting setting)
        {
            return IsInRange(setting.MasterVolume, 0f, 1f)
                && IsInRange(setting.BgmVolume, 0f, 1f)
                && IsInRange(setting.SeVolume, 0f, 1f)
                && IsInRange(setting.VoiceVolume, 0f, 1f)
                && IsInRange(setting.AudioPlaybackSpeed, 0.5f, 2f);
        }

        private static bool IsValid(DisplaySetting setting)
        {
            return Enum.IsDefined(typeof(PixelSize),setting.PixelSize)
                && Enum.IsDefined(typeof(FullScreenMode), setting.FullScreenMode)
                && (setting.TargetFrameRate == -1 || setting.TargetFrameRate >= 1);
        }

        private static bool IsValid(InputSetting setting)
        {
            return IsFinite(setting.StepAdvanceInterval)
                && setting.StepAdvanceInterval >= 0f;
        }

        private static bool IsValid(TextSetting setting)
        {
            return IsFinite(setting.CharactersPerSecond)
                && setting.CharactersPerSecond >= 0f
                && IsFinite(setting.AutoAdvanceDelay)
                && setting.AutoAdvanceDelay >= 0f
                && IsInRange(setting.TextPlaybackSpeed, 0.5f, 2f);
        }

        private static bool IsInRange(float value, float minimum, float maximum)
        {
            return IsFinite(value) && value >= minimum && value <= maximum;
        }

        private static bool IsFinite(float value)
        {
            return !float.IsNaN(value) && !float.IsInfinity(value);
        }

        private static SettingChangeScope GetChangeScope(
            SettingData current,
            SettingData updated)
        {
            SettingChangeScope scope = SettingChangeScope.None;

            if (!AreEqual(current.AudioSetting, updated.AudioSetting))
            {
                scope |= SettingChangeScope.Audio;
            }

            if (!AreEqual(current.DisplaySetting, updated.DisplaySetting))
            {
                scope |= SettingChangeScope.Display;
            }

            if (!AreEqual(current.InputSetting, updated.InputSetting))
            {
                scope |= SettingChangeScope.Input;
            }

            if (!AreEqual(current.TextSetting, updated.TextSetting))
            {
                scope |= SettingChangeScope.Text;
            }

            return scope;
        }

        private static bool AreEqual(AudioSetting left, AudioSetting right)
        {
            return left.MasterVolume == right.MasterVolume
                && left.BgmVolume == right.BgmVolume
                && left.SeVolume == right.SeVolume
                && left.VoiceVolume == right.VoiceVolume
                && left.AudioPlaybackSpeed == right.AudioPlaybackSpeed;
        }

        private static bool AreEqual(DisplaySetting left, DisplaySetting right)
        {
            return left.PixelSize == right.PixelSize
                && left.FullScreenMode == right.FullScreenMode
                && left.TargetFrameRate == right.TargetFrameRate;
        }

        private static bool AreEqual(InputSetting left, InputSetting right)
        {
            return left.StepAdvanceInterval == right.StepAdvanceInterval;
        }

        private static bool AreEqual(TextSetting left, TextSetting right)
        {
            return left.TypewriterEnabled == right.TypewriterEnabled
                && left.CharactersPerSecond == right.CharactersPerSecond
                && left.AutoAdvanceDelay == right.AutoAdvanceDelay
                && left.TextPlaybackSpeed == right.TextPlaybackSpeed;
        }
    }
}
