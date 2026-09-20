using Setting.Running;
using System;

namespace Setting.Events
{
    [Flags]
    public enum SettingChangeScope
    {
        None = 0,
        Audio = 1 << 0,
        Display = 1 << 1,
        Input = 1 << 2,
        Text = 1 << 3
    }

    public sealed class ESettingChanged
    {
        public SettingData SettingData { get; }
        public SettingChangeScope Scope { get; }

        public ESettingChanged(
            SettingData settingData,
            SettingChangeScope scope)
        {
            SettingData = settingData;
            Scope = scope;
        }
    }
}
