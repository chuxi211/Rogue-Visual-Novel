using Input;
using MainMenu.Input;

namespace MainMenu
{
    public class CancelSettting : SettingInput, IClick
    {
        public void Click()
        {
            EventBus.Publish(new ERequestSettingOperation(SettingOperation.Cancel));
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