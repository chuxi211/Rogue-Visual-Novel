using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Config
{
    [Serializable]
    [CreateAssetMenu(fileName ="New LayerMap",menuName ="ConfigInfo/Rogue/LayerMap")]
    public class LayerMapGroup : ScriptableObject
    {
        public int Layer;
        public List<Map> Variants = new List<Map>();
    }
}
