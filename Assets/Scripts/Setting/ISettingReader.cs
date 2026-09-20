using Setting.Running;

namespace Setting
{
    public interface IAudioReader
    {
        AudioSetting GetAudioSetting();
    }
    public interface IDisplayReader
    {
        DisplaySetting GetDisplaySetting();
    }
    public interface ITextReader
    {
        TextSetting GetTextSetting();
    }
    public interface IInputReader
    {
        InputSetting GetInputSetting();
    }
}
