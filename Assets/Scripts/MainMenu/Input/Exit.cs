namespace MainMenu.Input
{
    public class Exit : MainMenuInput
    {
        public override void Click()
        {
            base.Click();
            EventBus.Publish(new ERequestExitGame());
        }
    }
}