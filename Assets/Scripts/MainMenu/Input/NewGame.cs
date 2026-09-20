//New input
namespace MainMenu.Input
{
    public class NewGame : MainMenuInput
    {
        //static Instance隐式依赖
        public override void Click()
        {
            EventBus.Publish(new ERequestNewGame());
        }

        //通过Unity Inspector 显式绑定OnClick
        //或直接实现IPointerClick
    }
}