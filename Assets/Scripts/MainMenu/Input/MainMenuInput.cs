using Audio;
using Input;
using System;
using UnityEngine;
namespace MainMenu.Input
{
    [Serializable]
    public class MainMenuInput : MonoBehaviour, IClick
    {
        protected InputManager InputManager;
        protected ISoundPlayback SoundPlayback;
        protected AudioID AudioID;
        public virtual void Click()
        {
            Debug.LogError("The Method is not implement in MainMenuInput!\nTry to use NewGame?");
        }
        public void BindInput(InputManager inputManager)
        {
            InputManager = inputManager;
        }
        public void OnClick()
        {
            if (InputManager == null)
            {
                Debug.LogError(
                      $"{GetType().Name} failed: InputManager is null.");
                return;

            }
            InputManager.Push(this);
            SoundPlayback?.PlaySound(AudioID);
        }
        public void InitSE(AudioID audioID, ISoundPlayback soundPlayback)
        {
            if (audioID == null || !audioID.IsValid)
            {
                Debug.LogError(
                    "UIButton audio initialization failed: " +
                    "AudioID is invalid.");
                return;
            }

            if (soundPlayback == null)
            {
                Debug.LogError(
                    "UIButton audio initialization failed: " +
                    "SoundPlayback is null.");
                return;
            }
            AudioID = audioID;
            SoundPlayback = soundPlayback;
        }

        public void Execute()
        {
            Click();
        }
    }
}