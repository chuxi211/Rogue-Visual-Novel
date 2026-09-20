using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class AudioDefinitionRepository
    {
        private readonly Dictionary<AudioID, AudioDefinition> AudioDefinitions = new Dictionary<AudioID, AudioDefinition>();
        public bool IsValid { get; private set; } = true;

        public AudioDefinitionRepository(AudioDefinition[] definitions)
        {
            if (definitions == null)
            {
                Debug.LogError(
                    "AudioDefinitionRepository initialization failed: " +
                    "Definitions is null.");
                IsValid = false;
                return;
            }

            foreach (AudioDefinition definition in definitions)
            {
                if (!TryAdd(definition))
                {
                    IsValid = false;
                }
            }
        }

        public void Add(AudioDefinition audioDefinition)
        {
            if (!TryAdd(audioDefinition))
            {
                IsValid = false;
            }
        }

        public bool TryGetAudioDefinition(AudioID audioID, out AudioDefinition audioDefinition)
        {
            audioDefinition = null;

            if (audioID == null || !audioID.IsValid)
            {
                Debug.LogError(
                    "AudioDefinitionRepository lookup failed: " +
                    "AudioID is null or invalid.");
                return false;
            }

            return AudioDefinitions.TryGetValue(audioID, out audioDefinition);
        }

        private bool TryAdd(AudioDefinition audioDefinition)
        {
            if (audioDefinition == null)
            {
                Debug.LogError(
                    "AudioDefinitionRepository registration failed: " +
                    "AudioDefinition is null.");
                return false;
            }

            if (audioDefinition.ID == null || !audioDefinition.ID.IsValid)
            {
                Debug.LogError(
                    "AudioDefinitionRepository registration failed: " +
                    $"AudioID on '{audioDefinition.name}' is null or invalid.");
                return false;
            }

            if (AudioDefinitions.ContainsKey(audioDefinition.ID))
            {
                Debug.LogError(
                    "AudioDefinitionRepository registration failed: " +
                    $"AudioID '{audioDefinition.ID.ID}' is duplicated.");
                return false;
            }

            AudioDefinitions.Add(audioDefinition.ID, audioDefinition);
            return true;
        }
    }
}
