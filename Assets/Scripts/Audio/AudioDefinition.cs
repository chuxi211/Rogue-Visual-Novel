using System;
using UnityEngine;

namespace Audio
{
    [Serializable]
    [CreateAssetMenu(fileName = "AudioDefinition", menuName = "ConfigInfo/Audio/AudioDefinition", order = 1)]
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
        Sound,
        Music,
        Dialogue,
    }
    [Serializable]
    public class AudioID : IEquatable<AudioID>
    {
        public string ID;
        public bool IsValid => !string.IsNullOrEmpty(ID);

        public bool Equals(AudioID other)
        {
            return other != null && ID == other.ID;
        }
        public static bool operator ==(AudioID left, AudioID right)
        {
            if (left is null && right is null) return true;
            if (left is null || right is null) return false;
            return left.Equals(right);
        }
        public static bool operator !=(AudioID left, AudioID right)
        {
            return !(left == right);
        }
        public override bool Equals(object obj)
        {
            return obj is AudioID other && Equals(other);
        }
        public override int GetHashCode()
        {
            return ID != null ? ID.GetHashCode() : 0;
        }
    }
}