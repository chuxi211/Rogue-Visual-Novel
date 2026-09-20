namespace MainMenu.Input
{
    public class Setting : MainMenuInput
    {
        public override void Click()
        {
            EventBus.Publish(new ERequestSetting());
        }
    }
}