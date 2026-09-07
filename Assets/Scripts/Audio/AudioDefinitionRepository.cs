using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class AudioDefinitionRepository
    {
        private Dictionary<AudioID, AudioDefinition> audioDefinitions = new Dictionary<AudioID, AudioDefinition>();
        public AudioDefinitionRepository(AudioDefinition[] definitions)
        {
            foreach (var definition in definitions)
            {
                if (definition != null && definition.ID.IsValid)
                {
                    audioDefinitions[definition.ID] = definition;
                }
            }
        }
        public void Add(AudioDefinition audioDefinition)
        {
            if (audioDefinition != null && audioDefinition.ID.IsValid)
            {
                if(audioDefinitions.ContainsKey(audioDefinition.ID))
                {
                    Debug.LogWarning($"AudioDefinition with ID {audioDefinition.ID.ID} already exists. It will be overwritten.");
                }
                audioDefinitions[audioDefinition.ID] = audioDefinition;
            }
        }
        public bool TryGetAudioDefinition(AudioID audioID, out AudioDefinition audioDefinition)
        {
            return audioDefinitions.TryGetValue(audioID, out audioDefinition);
        }
    }
}