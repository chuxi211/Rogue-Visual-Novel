using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Audio
{
    //显式接口区分
    //或显式接口实现
    //或AudioDefinition Category判断
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
        void PlayVoice(AudioClip audioClip, float volume = 1.0f);
        void SetVoiceVolume(float volume);
    }
    public interface IVoicePlayback
    {
        void PlayVoice(AudioID audioID, float volume = 1.0f);
        void SetVoiceVolume(float volume);
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
    public interface IGetPlayVolume
    {
        float GetPlayVolume(float definitionVolume, float settingVolume);
    }
}
