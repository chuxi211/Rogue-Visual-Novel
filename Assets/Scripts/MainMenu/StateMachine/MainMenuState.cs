using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace StateMachine.Core
{
    public class MainMenuState : CoreState
    {
        private CanvasGroup Menu;
        public MainMenuState(CoreStateMachine stateMachine, CanvasGroup menu) : base(stateMachine)
        {
            Menu = menu;
        }
        public override void Enter()
        {
            EventBus.Subscript<ERequestNewGame>(OnNewGame);
            EventBus.Subscript<ERequestGallery>(OnGallery);
            if (Menu == null)
            {
                Debug.LogError("Menu is null");
                return;
            }
            Menu.blocksRaycasts=true;
            Menu.interactable=true;
        }
        public override void Exit()
        {
            EventBus.Unsubscript<ERequestNewGame>(OnNewGame);
            EventBus.Unsubscript<ERequestGallery>(OnGallery);
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
    }
}