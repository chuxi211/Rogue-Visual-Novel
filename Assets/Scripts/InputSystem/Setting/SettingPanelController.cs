using Setting;
using UnityEngine;

public sealed class SettingPanelController : MonoBehaviour
{
    private ISettingEditor SettingEditor;
    public void Open()
    {
        SettingEditor.BeginUpdate();
    }

    public void OnMasterVolumeChanged(float value)
    {
        SettingEditor.TryUpdateAudio(
            setting => setting.MasterVolume = value);
    }

    public void Confirm()
    {
        SettingEditor.ConfirmUpdate();
    }

    public void Cancel()
    {
        SettingEditor.CancelUpdate();
    }
}