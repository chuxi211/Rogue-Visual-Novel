using UnityEngine;

namespace StateMachine.Core
{
    public class GalleryState : CoreState
    {
        private CanvasGroup CGCanvas;
        private CGPlayer CGPlayer;
        public GalleryState(CoreStateMachine stateMachine,CanvasGroup canvas,CGPlayer player) : base(stateMachine)
        {
            CGCanvas = canvas;
            CGPlayer = player;
        }
        public override void Enter()
        {
            base.Enter();
            CGPlayer.InitAttribute(StateMachine.Context.SettingRepository.SettingData.OtherSetting.CGCount,
                StateMachine.Context.SettingRepository.SettingData.OtherSetting.CountPerPage);
            EventBus.Subscript<ERequestCloseGallery>(OnCloseGallery);
            if (CGCanvas == null)
            {
                Debug.LogError("CanvasGroup is null");
                return;
            }
            CGCanvas.alpha = 1;
            CGCanvas.blocksRaycasts = true;
            CGCanvas.interactable = true;
        }
        public override void Exit()
        {
            base.Exit();
            EventBus.Unsubscript<ERequestCloseGallery>(OnCloseGallery);
        }
        private void OnCloseGallery(ERequestCloseGallery e)
        {
            CGCanvas.alpha = 0;
            CGCanvas.blocksRaycasts = false;
            CGCanvas.interactable = false;
        }
    }
}