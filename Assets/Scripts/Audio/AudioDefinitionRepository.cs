using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class AudioDefinitionRepository
    {
        private Dictionary<AudioID, AudioDefinition> audioDefinitions = new Dictionary<AudioID, AudioDefinition>();
        public bool TryGetAudioDefinition(AudioID audioID, out AudioDefinition audioDefinition)
        {
            return audioDefinitions.TryGetValue(audioID, out audioDefinition);
        }
    }
}