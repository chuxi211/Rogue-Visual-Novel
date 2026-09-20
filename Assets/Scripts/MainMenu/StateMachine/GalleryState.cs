using UnityEngine;

namespace StateMachine.Core
{
    public class GalleryState : CoreState
    {
        private CanvasGroup GalleryCnvsGrp;
        private CGPlayer CGPlayer;
        public GalleryState(CoreStateMachine stateMachine, CanvasGroup canvas, CGPlayer player) : base(stateMachine)
        {
            GalleryCnvsGrp = canvas;
            CGPlayer = player;
        }
        public override void Enter()
        {
            base.Enter();
            EventBus.Subscript<ERequestCloseGallery>(OnCloseGallery);
            if (GalleryCnvsGrp == null)
            {
                Debug.LogError("CanvasGroup is null");
                return;
            }
            GalleryCnvsGrp.alpha = 1;
            GalleryCnvsGrp.blocksRaycasts = true;
            GalleryCnvsGrp.interactable = true;
            CGPlayer.Show();
        }
        public override void Exit()
        {
            base.Exit();
            EventBus.Unsubscript<ERequestCloseGallery>(OnCloseGallery);
        }
        private void OnCloseGallery(ERequestCloseGallery e)
        {
            GalleryCnvsGrp.alpha = 0;
            GalleryCnvsGrp.blocksRaycasts = false;
            GalleryCnvsGrp.interactable = false;
            CGPlayer.Hide();
            StateMachine.SwitchState<MainMenuState>();
        }
    }
}