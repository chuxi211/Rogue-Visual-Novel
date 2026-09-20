namespace MainMenu.Input
{
    public class Gallery : MainMenuInput
    {
        public override void Click()
        {
            EventBus.Publish(new ERequestGallery());
        }
    }
}