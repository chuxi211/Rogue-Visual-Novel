using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    [Serializable]
    [CreateAssetMenu(fileName = "AudioDefinition", menuName = "ConfigInfo/Audio", order = 1)]
    public class AudioDefinition : ScriptableObject
    {
        public AudioID ID;
        public string BundleName;
        public string AssetName;
        public AudioCategory Category;
        public float volume = 1.0f;
        public bool loop = false;
    }
    public enum AudioCategory
    {
        UI,
        Music,
        Dialogue,
    }
    [Serializable]
    public class AudioID
    {
        public string ID;
        public bool IsValid => !string.IsNullOrEmpty(ID);
    }
}