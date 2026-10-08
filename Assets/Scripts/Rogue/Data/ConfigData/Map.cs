using System;
using System.Collections.Generic;
using UnityEngine;

namespace Config
{
    [Serializable]
    [CreateAssetMenu(fileName = "Map", menuName = "ConfigInfo/Rogue/Map")]
    //后续需补充ID
    //用于持久化存档
    public class Map : ScriptableObject
    {
        public MapID ID;
        public List<Node> Nodes;
        public List<Edge> Edges;
    }
    [Serializable]
    public struct MapID : IEquatable<MapID>
    {
        [SerializeField]
        private int Layer;
        [SerializeField]
        private int VariantIndex;
        public int LayerNumber => Layer;
        public bool IsValid => Layer > 0 && VariantIndex >= 0;
        public string Value
        {
            get
            {
                return $"{Layer}_{VariantIndex}";
            }
        }
        public bool Equals(MapID other)
        {
            return Layer == other.Layer && VariantIndex == other.VariantIndex;
        }
        public override bool Equals(object obj)
        {
            return obj is MapID other && Equals(other);
        }
        public override int GetHashCode()
        {
            return Value == null ? 0 : Value.GetHashCode();
        }
        public static bool operator ==(MapID left, MapID right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(MapID left, MapID right)
        {
            return !left.Equals(right);
        }
    }
}
