using MainMenu;
using MainMenu.Input;
using UnityEngine;

namespace StateMachine.Core
{
    public class SavedModeState : CoreState
    {
        private CanvasGroup CanvasGroup;
        private SavedInfoPanel SavedInfoPanel;
        private ModeOfEnterSaveMode EnterSaveMode;
        public SavedModeState(CoreStateMachine stateMachine,CanvasGroup canvasGroup,SavedInfoPanel savedInfoPanel) : base(stateMachine)
        {
            CanvasGroup = canvasGroup;
            SavedInfoPanel = savedInfoPanel;
        }
        public override void Enter()
        {
            base.Enter();
            CanvasGroup.alpha = 1;
            CanvasGroup.blocksRaycasts = true;
            CanvasGroup.interactable = true;
            StateMachine.Context.InputManager.Push(new SavedPanelInputState());
            SavedInfoPanel.SetEnterModeAndShow(EnterSaveMode);
        }
        public override void Exit()
        {
            base.Exit();
            CanvasGroup.alpha = 0;
            CanvasGroup.blocksRaycasts = false;
            CanvasGroup.interactable = false;
            StateMachine.Context.InputManager.Pop();
            SavedInfoPanel.Hide();
        }
        public void SetEnterMode(ModeOfEnterSaveMode enterMode)
        {
            EnterSaveMode = enterMode;
        }
    }
}