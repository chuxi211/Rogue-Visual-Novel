using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Audio
{
    public class SoundPlayer : MonoBehaviour,ISoundPlayer
    {
        [SerializeField] private AudioSource AudioSource;

        public void PlaySound(AudioClip audio, float volume = 1)
        {
            AudioSource.PlayOneShot(audio, volume);
        }

        public void SetSoundVolume(float volume)
        {
            throw new System.NotImplementedException();
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