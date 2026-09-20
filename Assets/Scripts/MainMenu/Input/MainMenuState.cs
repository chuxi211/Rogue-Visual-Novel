using Input;
//主界面，该场景下只执行不缓存
namespace MainMenu.Input
{
    public class MainMenuState : IInputState
    {
        public bool CanBuffer(InputCommand command)
        {
            return false;
        }

        public bool CanHandle(InputCommand command)
        {
            return command != null && command.Input is MainMenuInput;
        }

        public void Handle(InputCommand command)
        {
            if (command?.Input is not MainMenuInput input)
            {
                return;
            }
            input.Click();
        }
    }
}