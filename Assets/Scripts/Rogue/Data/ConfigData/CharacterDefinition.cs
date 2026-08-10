using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName ="CharacterDefinition",menuName = "ConfigInfo/Characters/CharacterDefinition")]
    [Serializable]
    public class CharacterDefinition : ScriptableObject
    {
        public string ID;
        public string Name;
        public List< CharacterAppearance > Appearances;
    }
    [Serializable]
    public class CharacterAppearance
    {
        public VisualState VisualState;
        public Sprite Illustration;
    }
    [Serializable]
    public enum VisualState
    {
        Null=0,
        Black=1,
        Normal=2,
        Angry=3,
        Weeping = 4,
    }
}
