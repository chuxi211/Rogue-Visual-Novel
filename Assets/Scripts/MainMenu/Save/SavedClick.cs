using Input;

namespace MainMenu.Input
{
    public class SavedClick : SavedPanelInput
    {
        private InputManager InputManager;
        private ModeOfEnterSaveMode Mode;
        private int Index;
        public void SetEnterMode(ModeOfEnterSaveMode mode)
        {
            Mode = mode;
        }
        public void Init(InputManager inputManager,int index)
        {
            InputManager = inputManager;
            Index = index;
        }
        public override void Click()
        {
            //Input作为一种请求，不应直接调用(De)Serializer
            EventBus.Publish(new ESavedSlotRequestion(Mode,Index));
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