namespace MainMenu.Input
{
    public class Continue : MainMenuInput
    {
        public ModeOfEnterSaveMode EnterSaveMode;
        public override void Click()
        {
            EventBus.Publish(new ERequestSavedPanel(EnterSaveMode));

        }
    }
}