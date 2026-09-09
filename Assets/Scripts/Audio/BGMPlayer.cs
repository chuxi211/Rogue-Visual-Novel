using UnityEngine;

namespace Audio
{
    public class BGMPlayer : AudioPlayer, IMusicPlayer
    {
        [SerializeField] private AudioSource AudioSource;
        public void PlayMusic(AudioClip audioClip, float volume = 1)
        {
            if (AudioSource == null)
            {
                Debug.LogError(
                    "BGMPlayer failed to play voice: " +
                    "AudioSource is null.");
                return;
            }
            if (audioClip == null)
            {
                Debug.LogError(
                    "BGMPlayer failed to play music: " +
                    "AudioClip is null.");
                return;
            }
            if (audioClip == AudioSource.clip && AudioSource.isPlaying)
            {
                Debug.LogWarning(
                    "The auido you want to change is Equals with current:"); 
                return;
            }
            AudioSource.clip = audioClip;
            AudioSource.Play();
        }

        public void SetMusicVolume(float volume)
        {
            AudioSource.volume = volume;
        }

        public void StopMusic()
        {
            AudioSource.Stop();
        }
        public void OnEnable()
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