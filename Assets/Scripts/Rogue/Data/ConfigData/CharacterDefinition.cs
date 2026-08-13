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
        public Sprite GetIllustration(VisualState state)
        {
            var appearance = Appearances.Find(a => a.VisualState == state);
            if (appearance != null)
            {
                return appearance.Illustration;
            }
            else
            {
                Debug.LogWarning($"CharacterDefinition: No illustration found for VisualState {state} in character {Name}.");
                return null;
            }
        }
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
        //为Null时，表示当前隐藏
        Null=0,
        //黑色剪影
        Black=1,
        Normal=2,
        Angry=3,
        Weeping = 4,
    }
}
