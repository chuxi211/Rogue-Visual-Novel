using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Audio
{
    public interface ISoundPlayer
    {
        void PlaySound(AudioClip audioClip, float volume = 1.0f);
        void SetSoundVolume(float volume);
    }
    public interface ISoundPlayback
    {
        void PlaySound(AudioID audioID, float volume = 1.0f);
        void SetSoundVolume(float volume);
    }
    public interface IVoicePlayer
    {
        void PlaySound(AudioClip audioClip, float volume = 1.0f);
        void SetSoundVolume(float volume);
    }
    public interface IVoicePlayback
    {
        void PlaySound(AudioID audioID, float volume = 1.0f);
        void SetSoundVolume(float volume);
    }
    public interface IMusicPlayer
    {
        void PlayMusic(AudioClip audioClip, float volume = 1.0f);
        void StopMusic();
        void SetMusicVolume(float volume);
    }
    public interface IMusicPlayback
    {
        void PlayMusic(AudioID audioID, float volume = 1.0f);
        void StopMusic();
        void SetMusicVolume(float volume);
    }
}
