using Input;

namespace MainMenu.Input
{
    public class SavedPanelInputState : IInputState
    {
        public bool CanBuffer(InputCommand command)
        {
            return false;
        }

        public bool CanHandle(InputCommand command)
        {
            return command?.Input is SavedPanelInput;
        }

        public void Handle(InputCommand command)
        {
            if (command?.Input is SavedPanelInput savedPanelInput)
            {
                savedPanelInput.Execute();
            }
        }
    }
}