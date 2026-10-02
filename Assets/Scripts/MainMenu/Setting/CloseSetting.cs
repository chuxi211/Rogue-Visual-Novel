using Input;
using MainMenu.Input;

namespace MainMenu
{
    public class CloseSetting : SettingInput, IClick
    {
        public void Click()
        {
            EventBus.Publish(new ERequestSettingOperation(SettingOperation.Close));
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