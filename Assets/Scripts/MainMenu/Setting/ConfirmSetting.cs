using Input;
using MainMenu.Input;

namespace MainMenu
{
    public class ConfirmSetting : SettingInput, IClick
    {
        public void Click()
        {
            EventBus.Publish(new ERequestSettingOperation(SettingOperation.Confirm));
        }

        public override void Execute()
        {
            Click();
        }
        public void OnClick()
        {
            TryPush();
        }
    }
}