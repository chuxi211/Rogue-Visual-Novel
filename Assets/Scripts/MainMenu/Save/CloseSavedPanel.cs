using Input;

namespace MainMenu.Input
{
    public class CloseSavedPanel : SavedPanelInput
    {
        public override void Click()
        {
            EventBus.Publish(new ERequestCloseSavedPanel());
        }

        public override void Execute()
        {
            Click();
        }
        public void OnClick()
        {
            InputManager.Push(this);
        }
    }
}