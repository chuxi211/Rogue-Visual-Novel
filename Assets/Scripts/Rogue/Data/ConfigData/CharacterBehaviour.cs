using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "CharacterBehaviour", menuName = "ConfigInfo/Rogue/NodeBehaviours/CharacterBehaviour")]
    public class CharacterBehaviour : NodeBehaviour
    {
        public CharacterDefinition Character;
        public VisualState VisualState;
        [Range(0f, 1f)]
        public float Horizontal;
        [Range(0f, 1f)]
        public float Vertical;
        public Vector2 NormalizedPosition
        {
            get
            {
                return new Vector2(Horizontal, Vertical);
            }
        }
    }
}