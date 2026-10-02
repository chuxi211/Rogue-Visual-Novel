using Input;
using MainMenu.Input;

namespace MainMenu
{
    public class Exit : SettingInput, IClick
    {
        public void Click()
        {
            EventBus.Publish(new ERequestExitGame());
        }

        public override void Execute()
        {
            Click();
        }
    }
}