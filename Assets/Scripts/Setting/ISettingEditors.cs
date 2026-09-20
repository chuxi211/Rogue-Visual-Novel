using Setting.Running;
using System;

namespace Setting
{
    public interface IAudioEditor
    {
        bool TryUpdateAudio(Action<AudioSetting> update);
    }
    public interface IDisplayEditor
    {
        bool TryUpdateDisplay(Action<DisplaySetting> update);
    }
    public interface IInputEditor
    {
        bool TryUpdateInput(Action<InputSetting> update);
    }
    public interface ITextEditor
    {
        bool TryUpdateText(Action<TextSetting> update);
    }
    public interface ISettingEditor
    {
        void BeginUpdate();

        void ConfirmUpdate();
        void CancelUpdate();
    }
}