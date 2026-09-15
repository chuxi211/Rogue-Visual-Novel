using System;
using Setting.Running;

namespace Setting
{
    public interface ISettingEditor
    {
        void BeginUpdate();

        bool TryUpdateAudio(Action<AudioSetting> update);
        bool TryUpdateDisplay(Action<DisplaySetting> update);
        bool TryUpdateInput(Action<InputSetting> update);
        bool TryUpdateText(Action<TextSetting> update);

        void ConfirmUpdate();
        void CancelUpdate();
    }
}