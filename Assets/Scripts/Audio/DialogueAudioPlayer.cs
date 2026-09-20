using UnityEngine;

namespace Audio
{
    public class DialogueAudioPlayer : AudioPlayer, IVoicePlayer
    {
        [SerializeField] private AudioSource AudioSource;
        //当前没有对volume进行处理
        //后续接入Setting系统

        public void PlayVoice(AudioClip audio, float volume = 1)
        {
            if (AudioSource == null)
            {
                Debug.LogError(
                    "DialogueAudioPlayer failed to play voice: " +
                    "AudioSource is null.");
                return;
            }

            if (audio == null)
            {
                Debug.LogError(
                    "DialogueAudioPlayer failed to play voice: " +
                    "AudioClip is null.");
                return;
            }
            AudioSource.Stop();
            AudioSource.clip = audio;
            AudioSource.volume = volume;
            AudioSource.loop = false;
            AudioSource.Play();
        }

        public void SetVoiceVolume(float volume)
        {
            if (AudioSource == null)
            {
                Debug.LogError(
                    "DialogueAudioPlayer failed to set voice volume: " +
                    "AudioSource is null.");
                return;
            }

            AudioSource.volume = volume;
        }

        private void OnEnable()
        {
            if (AudioSource == null)
            {
                AudioSource = GetComponentInChildren<AudioSource>();
            }
            if (AudioSource == null)
            {
                Debug.LogError("AudioSource is not assigned and not found in children.");
            }
        }
    }
}
