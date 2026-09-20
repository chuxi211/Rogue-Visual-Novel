using System.Collections.Generic;
using UnityEngine;
namespace Audio
{
    [CreateAssetMenu(fileName = "AudioDefinitionCatalog", menuName = "ConfigInfo/Audio/AudioDefinitionCatalog")]
    public class AudioDefinitionCatalog : ScriptableObject
    {
        public List<AudioDefinition> AudioDefinitions = new List<AudioDefinition>();
        public void OnValidate()
        {
            foreach (var definition in AudioDefinitions)
            {
                if (definition == null)
                {
                    Debug.LogWarning("AudioDefinitionCatalog contains a null AudioDefinition.");
                }
            }
        }
    }
}