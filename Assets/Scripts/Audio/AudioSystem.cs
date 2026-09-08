using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Audio
{
    public class AudioSystem : IVoicePlayback, IMusicPlayback,ISoundPlayback
    {
        private readonly Dictionary<Type, AudioPlayer> AudioPlayers = new Dictionary<Type, AudioPlayer>();

        private readonly AudioCache AudioCache;
        private readonly AudioLoader AudioLoader;
        private readonly AudioDefinitionRepository AudioDefinitionRepository;
        public AudioSystem(List<AudioPlayer> audioPlayers, AssetBundleManager assetBundleManager, AudioDefinitionRepository audioDefinitionRepository)
        {

            //全局Player通过外部注入
            //局部Player通过AudioSystem.RegisterAudioPlayer注册


            if (audioPlayers != null)
            {
                foreach (AudioPlayer audioPlayer in audioPlayers)
                {
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

        public void PlayMusic(AudioID audioID, float volume = 1)
        {
            if(TryGetAudioPlayer<BGMPlayer>(out BGMPlayer bgmPlayer))
            {
                if (TryGetAudioClip(audioID, AudioCategory.Music, out AudioClip audioClip))
                {
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

        public void PlayVoice(AudioID audioID, float volume = 1)
        {
            if (TryGetAudioPlayer(out DialogueAudioPlayer dialogueAudioPlayer))
            {
                if (TryGetAudioClip(audioID, AudioCategory.Dialogue, out AudioClip audioClip))
                {
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


        public void SetMusicVolume(float volume)
        {
            if(TryGetAudioPlayer(out BGMPlayer bgmPlayer))
            {
                bgmPlayer.SetMusicVolume(volume);
            }
            else
            {
                Debug.LogError("BGMPlayer not registered in AudioSystem.");
            }
        }

        public void SetVoiceVolume(float volume)
        {
            if(TryGetAudioPlayer<DialogueAudioPlayer>(out DialogueAudioPlayer dialogueAudioPlayer))
            {
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
            }
            else
            {
                Debug.LogError("BGMPlayer not registered in AudioSystem.");
            }
        }

        private bool TryGetAudioClip(
            AudioID audioID,
            AudioCategory expectedCategory,
            out AudioClip audioClip)
        {
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
                    out AudioDefinition audioDefinition))
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

        public void PlaySound(AudioID audioID, float volume = 1)
        {
            if(TryGetAudioPlayer<SoundPlayer>(out SoundPlayer soundPlayer))
            {
                if (TryGetAudioClip(audioID, AudioCategory.Sound, out AudioClip audioClip))
                {
                    soundPlayer.PlaySound(audioClip, volume);
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

        public void SetSoundVolume(float volume)
        {
            if(TryGetAudioPlayer<SoundPlayer>(out SoundPlayer soundPlayer))
            {
                soundPlayer.SetSoundVolume(volume);
            }
            else
            {
                Debug.LogError("SoundPlayer not registered in AudioSystem.");
            }
        }
    }
}
