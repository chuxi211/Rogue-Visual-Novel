using MainMenu;
using UnityEngine;

namespace StateMachine.Core
{
    public class SettingState : CoreState
    {
        private CanvasGroup Setting;
        public SettingState(CoreStateMachine stateMachine, CanvasGroup canvasGroup) : base(stateMachine)
        {
            Setting = canvasGroup;
        }
        public override void Enter()
        {
            if (Setting == null)
            {
                Debug.LogError(
                    "SettingState failed to enter: " +
                    "Setting CanvasGroup is null.");
                return;
            }

            if (StateMachine?.Context == null)
            {
                Debug.LogError(
                    "SettingState failed to enter: " +
                    "CoreContext is null.");
                return;
            }

            if (!RefreshUIAndBeginUpdate())
            {
                return;
            }
            
            EventBus.Subscript<ERequestSettingOperation>(OnRequestSettingOperation);
            StateMachine.Context.InputManager.Push(new MainMenu.Input.SettingState());
            Setting.alpha = 1f;
            Setting.interactable = true;
            Setting.blocksRaycasts = true;
            //Pop 需要后续CloseSetting+EventBus接入
            //Confirm & Cancel需要等待UI接入
        }
        public override void Exit()
        {
            base.Exit();
            Setting.alpha = 0f;
            Setting.interactable = false;
            Setting.blocksRaycasts = false;
            EventBus.Unsubscript<ERequestSettingOperation>(OnRequestSettingOperation);
            CoreContext context = StateMachine.Context;
            context.InputManager.Pop();
        }
        private bool RefreshUIAndBeginUpdate()
        {
            CoreContext context = StateMachine.Context;
            if (context.InputManager == null || context.SettingService == null ||
                context.AudioArea == null || context.DisplayArea == null ||
                context.InputArea == null || context.TextArea == null)
            {
                Debug.LogError(
                    "SettingState failed to enter: " +
                    "one or more setting dependencies are null.");
                return false;
            }

            context.SettingService.BeginUpdate();

            bool valuesInitialized = true;
            valuesInitialized &= context.AudioArea.SetValue();
            valuesInitialized &= context.DisplayArea.SetValue();
            valuesInitialized &= context.InputArea.SetValue();
            valuesInitialized &= context.TextArea.SetValue();

            if (!valuesInitialized)
            {
                context.SettingService.CancelUpdate();
                Debug.LogError(
                    "SettingState failed to enter: " +
                    "setting UI values could not be initialized.");
                return false;
            }
            return valuesInitialized;
        }
        private void OnRequestSettingOperation(ERequestSettingOperation operation)
        {
            if (operation == null)
            {
                return;
            }
            switch (operation.SettingOperation)
            {
                case SettingOperation.Cancel:
                    StateMachine.Context.SettingService.CancelUpdate();
                    RefreshUIAndBeginUpdate();
                    return;
                case SettingOperation.Close:
                    StateMachine.Context.SettingService.CancelUpdate();
                    StateMachine.SwitchState<MainMenuState>();
                    return;
                case SettingOperation.Confirm:
                    if (!StateMachine.Context.SettingService.ConfirmUpdate())
                    {
                        StateMachine.Context.SettingService.CancelUpdate();

                        if (!RefreshUIAndBeginUpdate())
                        {
                            Debug.LogError(
                                "SettingState failed to recover after confirm failure.");
                        }

                        return;
                    }

                    StateMachine.Context.SettingService.BeginUpdate();
                    return;
                default:
                    return;
            }
        }
    }
}
