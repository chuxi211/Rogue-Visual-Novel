using Input;
using UnityEngine;
namespace StateMachine.Core
{
    public class MainMenuState : CoreState
    {
        private CanvasGroup Menu;
        private InputManager InputManager { get => StateMachine.Context.InputManager; }
        public MainMenuState(CoreStateMachine stateMachine, CanvasGroup menu) : base(stateMachine)
        {
            Menu = menu;
        }
        public override void Enter()
        {
            EventBus.Subscript<ERequestEnterRogue>(OnNewGame);
            EventBus.Subscript<ERequestGallery>(OnGallery);
            EventBus.Subscript<ERequestSetting>(OnSetting);
            EventBus.Subscript<ERequestSavedPanel>(OnSaved);
            if (Menu == null)
            {
                Debug.LogError("Menu is null");
                return;
            }
            Menu.blocksRaycasts = true;
            Menu.interactable = true;
            InputManager.Switch(new MainMenu.Input.MainMenuState());
        }
        public override void Exit()
        {
            EventBus.Unsubscript<ERequestEnterRogue>(OnNewGame);
            EventBus.Unsubscript<ERequestGallery>(OnGallery);
            EventBus.Unsubscript<ERequestSetting>(OnSetting);
            EventBus.Unsubscript<ERequestSavedPanel>(OnSaved);
        }
        public override void Resume()
        {
            EventBus.Subscript<ERequestEnterRogue>(OnNewGame);
            EventBus.Subscript<ERequestGallery>(OnGallery);
            EventBus.Subscript<ERequestSetting>(OnSetting);
            EventBus.Subscript<ERequestSavedPanel>(OnSaved);
        }
        public override void Suspend()
        {
            EventBus.Unsubscript<ERequestEnterRogue>(OnNewGame);
            EventBus.Unsubscript<ERequestGallery>(OnGallery);
            EventBus.Unsubscript<ERequestSetting>(OnSetting);
            EventBus.Unsubscript<ERequestSavedPanel>(OnSaved);
        }
        private void OnNewGame(ERequestEnterRogue e)
        {
            StateMachine.SwitchState(new RogueModeState(StateMachine));
        }
        private void OnGallery(ERequestGallery e)
        {
            Menu.interactable = false;
            Menu.blocksRaycasts = false;
            StateMachine.SwitchState<GalleryModeState>();
        }
        private void OnSetting(ERequestSetting e)
        {
            Menu.interactable = false;
            Menu.blocksRaycasts = false;
            StateMachine.SwitchState<SettingState>();
        }
        private void OnSaved(ERequestSavedPanel e)
        {
            Menu.interactable = false;
            Menu.blocksRaycasts = false;
            if(StateMachine.TryGetState<SavedModeState>(out var savedModeState))
            {
                savedModeState.SetEnterMode(e.ModeOfEnterSaveMode);
                savedModeState.SetSavedInfoProvider(null);
                StateMachine.SuspendState(savedModeState);
                return;
            }
        }
    }
}
