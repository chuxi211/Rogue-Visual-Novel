using MainMenu;
using MainMenu.Input;
using Running;
using System;
using UnityEngine;

namespace StateMachine.Core
{
    public class SavedModeState : CoreState
    {
        private CanvasGroup CanvasGroup;
        private SavedInfoPanel SavedInfoPanel;
        private ModeOfEnterSaveMode EnterSaveMode;
        private Func<SavedInfo> SavedInfoProvider;
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
            EventBus.Subscript<ERequestCloseSavedPanel>(OnClosePanel);
            EventBus.Subscript<ESavedSlotRequestion>(OnSavedSlotRequest);
        }
        public override void Exit()
        {
            base.Exit();
            CanvasGroup.alpha = 0;
            CanvasGroup.blocksRaycasts = false;
            CanvasGroup.interactable = false;
            StateMachine.Context.InputManager.Pop();
            SavedInfoPanel.Hide();
            EventBus.Unsubscript<ESavedSlotRequestion>(OnSavedSlotRequest);
            EventBus.Unsubscript<ERequestCloseSavedPanel>(OnClosePanel);
            SavedInfoProvider = null;
        }
        public void SetEnterMode(ModeOfEnterSaveMode enterMode)
        {
            EnterSaveMode = enterMode;
        }
        public void SetSavedInfoProvider(Func<SavedInfo> provider)
        {
            SavedInfoProvider = provider;
        }
        private void OnClosePanel(ERequestCloseSavedPanel e)
        {
            StateMachine.ResumeState();
        }
        private void OnSavedSlotRequest(ESavedSlotRequestion e)
        {
            switch (e.Mode)
            {
                case ModeOfEnterSaveMode.Load:
                    StateMachine.Context.SavedInfoDeserializer.TryDeserialise(e.SlotIndex,out SavedInfo savedInfo);
                    StateMachine.SwitchState(new RogueModeState(StateMachine, EnterRogueMode.Load, savedInfo));
                    break;
                case ModeOfEnterSaveMode.Save:
                    if (e.SlotIndex < 0 || e.SlotIndex >= SavedInfoPanel.Slots.Count)
                    {
                        Debug.LogError($"Invalid save slot index: {e.SlotIndex}");
                        break;
                    }
                    SavedInfo currentInfo = SavedInfoProvider?.Invoke();
                    if (currentInfo == null)
                    {
                        Debug.LogError("Cannot save: current Rogue state is unavailable.");
                        break;
                    }
                    SavedErrorCode result = StateMachine.Context.SavedInfoSerializer.TrySerialize(
                        currentInfo, e.SlotIndex, SaveMode.Manual);
                    if (result == SavedErrorCode.Success)
                    {
                        SavedInfoPanel.Refresh();
                    }
                    else
                    {
                        Debug.LogError($"Failed to save slot {e.SlotIndex}: {result}");
                    }
                    break;
            }
        }
    }
}
