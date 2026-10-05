namespace Rogue.Input
{
    public class Load : Rogue2DFunction
    {
        public ModeOfEnterSaveMode EnterSaveMode = ModeOfEnterSaveMode.Load;
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