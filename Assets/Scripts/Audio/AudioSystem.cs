using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Audio
{
    public class AudioSystem :ISoundPlayback, IVoicePlayback, IMusicPlayback
    {
        private DialogueAudioPlayer DialogueAudioPlayer;
        private SoundPlayer SoundPlayer;

        private AudioCache AudioCache;
        private AudioLoader AudioLoader;
        private AudioDefinitionRepository AudioDefinitionRepository;
        public AudioSystem(AssetBundleManager assetBundleManager, AudioDefinitionRepository audioDefinitionRepository)
        {
            //Player通过外部注入
            AudioCache = new AudioCache();
            AudioLoader = new AudioLoader(assetBundleManager);
            if(audioDefinitionRepository!= null)
            {
                AudioDefinitionRepository = audioDefinitionRepository;
            }
            else
            {
                Debug.LogError("AudioDefinitionRepository is null");
            }
        private bool GetAudioClip(AudioID audioID,out AudioClip audioClip)
        {
            audioClip = null;
            if(AudioCache.TryGetAudioClip(audioID, out audioClip))
            {
                return true;
            }
            if (AudioDefinitionRepository.TryGetAudioDefinition(audioID, out var audioDefinition))
            {
                AudioLoader.TryLoad(audioDefinition.BundleName,audioDefinition.AssetName,out audioClip);
                if (audioClip != null)
                {
                    AudioCache.Add(audioID, audioClip);
                    return true;
                }
            }
            return false;
        }
        //考虑到Sound Effect的播放可能会有多个同时播放的情况，所以SoundPlayer应该是一个可以同时播放多个音效的播放器
        //Sound Effect和Voice(DialogueAudio)的播放方式是不同的，需要不同的播放器来处理。
        //已有ISoundPlayback和IVoicePlayback接口进行区分
        //要怎么动态选择使用哪个播放器？
        public void PlayMusic(AudioID audioID, float volume = 1)
        {
            if (GetAudioClip(audioID, out var audioClip))
            {
                //MusicPlayer.PlayMusic(audioClip, volume);
            }
        }

        public void PlaySound(AudioID audioID, float volume = 1)
        {
            if(GetAudioClip(audioID, out var audioClip))
            {
                SoundPlayer.PlaySound(audioClip, volume);
            }
        }

        public void SetMusicVolume(float volume)
        {
            throw new System.NotImplementedException();
        }

        public void SetSoundVolume(float volume)
        {
            throw new System.NotImplementedException();
        }

        public void StopMusic()
        {
            throw new System.NotImplementedException();
        }
    }
}