namespace MainMenu
{
    public class ERequestSettingOperation 
    {
        public SettingOperation SettingOperation { get; set; }
        public ERequestSettingOperation(SettingOperation settingOperation)
        {
            SettingOperation = settingOperation;
        }
    }
    public enum SettingOperation
    {
        Close,
        Confirm,
        Cancel
    }
}