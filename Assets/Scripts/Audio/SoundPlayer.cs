using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Audio
{
    public class SoundPlayer :AudioPlayer,ISoundPlayer
    {
        [SerializeField] private AudioSource AudioSource;

        public void PlaySound(AudioClip audio, float volumescale = 1)
        {
            if (AudioSource == null)
            {
                Debug.LogError(
                    "SoundPlayer failed to play sound: " +
                    "AudioSource is null.");
                return;
            }

            if (audio == null)
            {
                Debug.LogError(
                    "SoundPlayer failed to play sound: " +
                    "AudioClip is null.");
                return;
            }

            AudioSource.PlayOneShot(audio,volumescale);
        }

        public void SetSoundVolume(float volume)
        {
            if (AudioSource == null)
            {
                Debug.LogError(
                    "SoundPlayer failed to set sound volume: " +
                    "AudioSource is null.");
                return;
            }

            AudioSource.volume = volume;
        }

        private void OnEnable()
        {
            if(AudioSource == null)
            {
                AudioSource = GetComponentInChildren<AudioSource>();
            }
            if(AudioSource == null)
            {
                Debug.LogError("AudioSource is not assigned and not found in children.");
            }
        }
    }
}
