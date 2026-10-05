using Input;
using Running;

namespace Rogue.Input
{
    public class Save :Rogue2DFunction
    {
        public ModeOfEnterSaveMode EnterSaveMode = ModeOfEnterSaveMode.Save;

        public override void Click()
        {
            EventBus.Publish(new ERequestSavedPanel(EnterSaveMode));
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