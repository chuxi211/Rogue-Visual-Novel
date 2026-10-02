using Input;
using MainMenu.Input;

namespace MainMenu
{
    public class ExitRogue : SettingInput, IClick
    {
        public void Click()
        {
            EventBus.Publish(new ERequestAbortRogue());
        }

        public override void Execute()
        {
            Click();
        }
    }
}