using Input;
using MainMenu.Input;

namespace MainMenu
{
    public class CloseSetting : SettingInput, IClick
    {
        public void Click()
        {
            EventBus.Publish(new ERequestCloseSetting());
        }

        public override void Execute()
        {
            Click();
        }
        public void OnClick()
        {
            if (InputManager == null)
            {
                return;
            }
            InputManager.Push(this);
        }
    }
}