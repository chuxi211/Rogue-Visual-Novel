using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class AudioCache
    {
        private Dictionary<AudioID,AudioClip> LoadedeAudios = new Dictionary<AudioID, AudioClip>();
        public bool Add(AudioID audioID, AudioClip audioClip)
        {
            if (LoadedeAudios.ContainsKey(audioID))
            {
                return false;
            }
            LoadedeAudios.Add(audioID, audioClip);
            return true;
        }
        public bool TryGetAudioClip(AudioID audioID,out AudioClip audioClip)
        {
            return LoadedeAudios.TryGetValue(audioID, out audioClip);
        }
    }
}