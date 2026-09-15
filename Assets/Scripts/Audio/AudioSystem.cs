using Setting.Events;
using Setting.Running;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Audio
{
    public class AudioSystem : IVoicePlayback, IMusicPlayback,ISoundPlayback,IGetPlayVolume
    {
        private readonly Dictionary<Type, AudioPlayer> AudioPlayers = new Dictionary<Type, AudioPlayer>();

        private readonly AudioCache AudioCache;
        private readonly AudioLoader AudioLoader;
        private readonly AudioDefinitionRepository AudioDefinitionRepository;
        private Setting.Running.AudioSetting AudioSetting;


        private AudioDefinition CurrentBGM;
        private AudioDefinition CurrentVoice;
        public AudioSystem(
            List<AudioPlayer> audioPlayers,
            AssetBundleManager assetBundleManager,
            AudioDefinitionRepository audioDefinitionRepository,
            AudioSetting audioSetting)
        {

            //全局Player通过外部注入
            //局部Player通过AudioSystem.RegisterAudioPlayer注册


            if (audioPlayers != null)
            {
                foreach (AudioPlayer audioPlayer in audioPlayers)
                {
                    if(audioPlayer != null)
                    RegisterAudioPlayer(audioPlayer);
                }
            }
            else
            {
                Debug.LogError("AudioPlayers is null");
            }
            AudioCache = new AudioCache();
            if (assetBundleManager != null)
            {
                AudioLoader = new AudioLoader(assetBundleManager);
            }
            else
            {
                Debug.LogError("AssetBundleManager is null");
            }

            if (audioDefinitionRepository != null)
            {
                AudioDefinitionRepository = audioDefinitionRepository;
            }
            else
            {
                Debug.LogError("AudioDefinitionRepository is null");
            }
            if (audioSetting == null)
            {
                Debug.LogError("AudioSetting is null");
            }
            else
            {
                ApplyAudioSetting(audioSetting);
            }
            Subscribe();
        }
        public void Subscribe()
        {
            EventBus.Subscript<ESettingChanged>(OnSettingChanged);
        }

        public void Dispose()
        {
            EventBus.Unsubscript<ESettingChanged>(OnSettingChanged);
        }
        public void RegisterAudioPlayer<T>(T audioPlayer) where T : AudioPlayer
        {
            RegisterAudioPlayer((AudioPlayer)audioPlayer);
        }

        private void RegisterAudioPlayer(AudioPlayer audioPlayer)
        {
            if (audioPlayer == null)
            {
                Debug.LogError("AudioPlayer registration failed: AudioPlayer is null.");
                return;
            }

            Type audioPlayerType = audioPlayer.GetType();

            if (AudioPlayers.TryGetValue(audioPlayerType, out AudioPlayer registeredPlayer))
            {
                if (registeredPlayer == null)
                {
                    AudioPlayers.Remove(audioPlayerType);
                }
                else if (registeredPlayer == audioPlayer)
                {
                    Debug.LogWarning(
                        "AudioPlayer registration skipped: " +
                        $"'{audioPlayerType.Name}' is already registered.");
                    return;
                }
                else
                {
                    Debug.LogError(
                        "AudioPlayer registration failed: " +
                        $"Another '{audioPlayerType.Name}' is already registered.");
                    return;
                }
            }

            AudioPlayers.Add(audioPlayerType, audioPlayer);
        }

        public void UnregisterAudioPlayer<T>() where T : AudioPlayer
        {
            Type audioPlayerType = typeof(T);

            if (!AudioPlayers.Remove(audioPlayerType))
            {
                Debug.LogWarning(
                    "AudioPlayer unregistration skipped: " +
                    $"'{audioPlayerType.Name}' is not registered.");
            }
        }
        private void OnSettingChanged(ESettingChanged e)
        {
            if (e == null || e.SettingData == null)
            {
                Debug.LogError(
                    "AudioSystem failed to apply changed settings: " +
                    "event or SettingData is null.");
                return;
            }

            if ((e.Scope & SettingChangeScope.Audio) == 0)
            {
                return;
            }

            ApplyAudioSetting(e.SettingData.AudioSetting);
        }

        private void ApplyAudioSetting(AudioSetting audioSetting)
        {
            if (audioSetting == null)
            {
                Debug.LogError(
                    "AudioSystem failed to apply AudioSetting: " +
                    "AudioSetting is null.");
                return;
            }

            AudioSetting = new AudioSetting(audioSetting);

            SetMusicVolume();
            SetVoiceVolume();
            SetSoundVolume();
        }
        public void PlayMusic(AudioID audioID)
        {
            if (!HasValidAudioSetting())
            {
                return;
            }

            if(TryGetAudioPlayer<BGMPlayer>(out BGMPlayer bgmPlayer))
            {
                if (TryGetAudio(audioID, AudioCategory.Music,out AudioDefinition audioDefinition, out AudioClip audioClip))
                {
                    float volume = GetBGMVolume(audioDefinition.volume, AudioSetting);
                    CurrentBGM = audioDefinition;
                    bgmPlayer.PlayMusic(audioClip, volume);
                }
                else
                {
                    Debug.LogError(
                        "AudioSystem failed to play music: " +
                        $"AudioClip for AudioID '{audioID?.ID}' was not found.");
                }
            }
            else
            {
                Debug.LogError("BGMPlayer not registered in AudioSystem.");
            }
        }

        public void PlayVoice(AudioID audioID)
        {
            if (!HasValidAudioSetting())
            {
                return;
            }

            if (TryGetAudioPlayer(out DialogueAudioPlayer dialogueAudioPlayer))
            {
                if (TryGetAudio(audioID, AudioCategory.Dialogue,out AudioDefinition audioDefinition, out AudioClip audioClip))
                {
                    float volume = GetVoiceVolume(audioDefinition.volume, AudioSetting);
                    CurrentVoice = audioDefinition;
                    dialogueAudioPlayer.PlayVoice(audioClip, volume);
                }
                else
                {
                    Debug.LogError(
                        "AudioSystem failed to play voice: " +
                        $"AudioClip for AudioID '{audioID?.ID}' was not found.");
                }
            }
            else
            {
                Debug.LogError("DialogueAudioPlayer not registered in AudioSystem.");
            }
        }


        public void SetMusicVolume()
        {
            if (!HasValidAudioSetting())
            {
                return;
            }

            if(TryGetAudioPlayer(out BGMPlayer bgmPlayer))
            {
                if(CurrentBGM == null)
                {
                    return;
                }
                float volume = GetBGMVolume(CurrentBGM.volume, AudioSetting);
                bgmPlayer.SetMusicVolume(volume);
            }
            else
            {
                Debug.LogError("BGMPlayer not registered in AudioSystem.");
            }
        }

        public void SetVoiceVolume()
        {
            if (!HasValidAudioSetting())
            {
                return;
            }

            if(TryGetAudioPlayer<DialogueAudioPlayer>(out DialogueAudioPlayer dialogueAudioPlayer))
            {
                if(CurrentVoice == null)
                {
                    return;
                }
                float volume = GetVoiceVolume(CurrentVoice.volume, AudioSetting);
                dialogueAudioPlayer.SetVoiceVolume(volume);
            }
            else
            {
                Debug.LogError("DialogueAudioPlayer not registered in AudioSystem.");
            }
        }

        public void StopMusic()
        {
            if(TryGetAudioPlayer(out BGMPlayer bgmPlayer))
            {
                bgmPlayer.StopMusic();
                CurrentBGM = null;
            }
            else
            {
                Debug.LogError("BGMPlayer not registered in AudioSystem.");
            }
        }

        private bool TryGetAudio(
            AudioID audioID,
            AudioCategory expectedCategory,
            out AudioDefinition audioDefinition,
            out AudioClip audioClip)
        {
            audioDefinition = null;
            audioClip = null;

            if (audioID == null || !audioID.IsValid)
            {
                Debug.LogError(
                    "AudioSystem failed to get AudioClip: " +
                    "AudioID is null or invalid.");
                return false;
            }

            if (AudioCache == null)
            {
                Debug.LogError(
                    "AudioSystem failed to get AudioClip: " +
                    "AudioCache is null.");
                return false;
            }

            if (AudioDefinitionRepository == null)
            {
                Debug.LogError(
                    "AudioSystem failed to get AudioClip: " +
                    "AudioDefinitionRepository is null.");
                return false;
            }

            if (!AudioDefinitionRepository.TryGetAudioDefinition(
                    audioID,
                    out audioDefinition))
            {
                Debug.LogError(
                    "AudioSystem failed to get AudioClip: " +
                    $"AudioDefinition for AudioID '{audioID.ID}' was not found.");
                return false;
            }

            if (audioDefinition == null)
            {
                Debug.LogError(
                    "AudioSystem failed to get AudioClip: " +
                    $"AudioDefinition for AudioID '{audioID.ID}' is null.");
                return false;
            }

            if (audioDefinition.Category != expectedCategory)
            {
                Debug.LogError(
                    "AudioSystem failed to get AudioClip: " +
                    $"AudioID '{audioID.ID}' has category '{audioDefinition.Category}', " +
                    $"but '{expectedCategory}' was expected.");
                return false;
            }

            if (AudioCache.TryGetAudioClip(audioID, out audioClip))
            {
                return true;
            }

            if (string.IsNullOrEmpty(audioDefinition.BundleName))
            {
                Debug.LogError(
                    "AudioSystem failed to get AudioClip: " +
                    $"BundleName for AudioID '{audioID.ID}' is null or empty.");
                return false;
            }

            if (string.IsNullOrEmpty(audioDefinition.AssetName))
            {
                Debug.LogError(
                    "AudioSystem failed to get AudioClip: " +
                    $"AssetName for AudioID '{audioID.ID}' is null or empty.");
                return false;
            }

            if (AudioLoader == null)
            {
                Debug.LogError(
                    "AudioSystem failed to get AudioClip: " +
                    "AudioLoader is null.");
                return false;
            }

            if (!AudioLoader.TryLoad(
                    audioDefinition.BundleName,
                    audioDefinition.AssetName,
                    out audioClip))
            {
                return false;
            }

            if (audioClip == null)
            {
                Debug.LogError(
                    "AudioSystem failed to get AudioClip: " +
                    $"Loaded AudioClip for AudioID '{audioID.ID}' is null.");
                return false;
            }

            AudioCache.Add(audioID, audioClip);
            return true;
        }

        private bool TryGetAudioPlayer<TAudioPlayer>(out TAudioPlayer audioPlayer)
            where TAudioPlayer : AudioPlayer
        {
            audioPlayer = null;
            Type audioPlayerType = typeof(TAudioPlayer);

            if (!AudioPlayers.TryGetValue(
                    audioPlayerType,
                    out AudioPlayer registeredPlayer))
            {
                return false;
            }

            if (registeredPlayer == null)
            {
                AudioPlayers.Remove(audioPlayerType);
                Debug.LogError(
                    "AudioPlayer lookup failed: " +
                    $"Registered '{audioPlayerType.Name}' is null or destroyed.");
                return false;
            }

            audioPlayer = registeredPlayer as TAudioPlayer;

            if (audioPlayer == null)
            {
                Debug.LogError(
                    "AudioPlayer lookup failed: " +
                    $"Registered Player cannot be cast to '{audioPlayerType.Name}'.");
                return false;
            }

            return true;
        }

        public void PlaySound(AudioID audioID)
        {
            if (!HasValidAudioSetting())
            {
                return;
            }

            if(TryGetAudioPlayer<SoundPlayer>(out SoundPlayer soundPlayer))
            {
                if (TryGetAudio(audioID, AudioCategory.Sound,out AudioDefinition audioDefinition, out AudioClip audioClip))
                {
                    soundPlayer.PlaySound(audioClip,audioDefinition.volume);
                }
                else
                {
                    Debug.LogError(
                        "AudioSystem failed to play sound: " +
                        $"AudioClip for AudioID '{audioID?.ID}' was not found.");
                }
            }
            else
            {
                Debug.LogError("SoundPlayer not registered in AudioSystem.");
            }
        }

        public void SetSoundVolume()
        {
            if (!HasValidAudioSetting())
            {
                return;
            }

            if(TryGetAudioPlayer<SoundPlayer>(out SoundPlayer soundPlayer))
            {
                float volume = AudioSetting.MasterVolume * AudioSetting.SeVolume;
                soundPlayer.SetSoundVolume(volume);
            }
            else
            {
                Debug.LogError("SoundPlayer not registered in AudioSystem.");
            }
        }

        public float GetPlayVolume(float definitionVolume, AudioSetting audioSetting)
        {
            return definitionVolume*audioSetting.MasterVolume;
        }

        public float GetBGMVolume(float definitionVolume, AudioSetting audioSetting)
        {
            return definitionVolume * audioSetting.MasterVolume * audioSetting.BgmVolume;
        }

        public float GetVoiceVolume(float definitionVolume, AudioSetting audioSetting)
        {
            return definitionVolume*audioSetting.MasterVolume*audioSetting.VoiceVolume;
        }

        public float GetSEVolume(float definitionVolume, AudioSetting audioSetting)
        {
            return audioSetting.MasterVolume*audioSetting.SeVolume;
        }

        private bool HasValidAudioSetting()
        {
            if (AudioSetting != null)
            {
                return true;
            }

            Debug.LogError(
                "AudioSystem failed to process audio volume: " +
                "AudioSetting is null.");
            return false;
        }
    }
}
