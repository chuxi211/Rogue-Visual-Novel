using Audio;
using UnityEngine;
namespace Utility
{
    public class UIButton : MonoBehaviour
    {
        protected ISoundPlayback SoundPlayback;
        protected AudioID AudioID;
        public virtual void OnClick()
        {
            Debug.LogWarning("UIButton OnClick is not implemented. Please use UIButton<T> instead.");
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
    }
    public class UIButton<T> : UIButton where T : new()
    {
        public override void OnClick()
        {
            EventBus.Publish(new T());
            SoundPlayback?.PlaySound(AudioID);
        }
    }

}