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
            EventBus.Subscript<ERequestNewGame>(OnNewGame);
            EventBus.Subscript<ERequestGallery>(OnGallery);
            EventBus.Subscript<ERequestSetting>(OnSetting);
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
            EventBus.Unsubscript<ERequestNewGame>(OnNewGame);
            EventBus.Unsubscript<ERequestGallery>(OnGallery);
            EventBus.Unsubscript<ERequestSetting>(OnSetting);
        }
        private void OnNewGame(ERequestNewGame e)
        {
            StateMachine.SwitchState<RogueModeState>();
        }
        private void OnGallery(ERequestGallery e)
        {
            Menu.interactable = false;
            Menu.blocksRaycasts = false;
            StateMachine.SwitchState<GalleryState>();
        }
        private void OnSetting(ERequestSetting e)
        {
            Menu.interactable = false;
            Menu.blocksRaycasts = false;
            StateMachine.SwitchState<SettingState>();
        }
    }
}