using System;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "Edge", menuName = "ConfigInfo/Rogue/Edge")]
    [Serializable]
    public class Edge : ScriptableObject
    {
        public Node From;
        public Node To;
        private bool IsValidPosition()
        {
            if (From == null || To == null) return false;
            int XOffset = To.Position.X - From.Position.X;
            int YOffset = To.Position.Y - From.Position.Y;
            bool IsVerticalNeighbour =
              XOffset == 0 &&
              Mathf.Abs(YOffset) == 1;
            bool IsHorizontalNeighbour =
              XOffset == 1 &&
              Mathf.Abs(YOffset) <= 1;
            return IsVerticalNeighbour || IsHorizontalNeighbour;
        }
        private void OnValidate()
        {
            if (From == null || To == null)
            {
                return;
            }

            if (!IsValidPosition())
            {
                Debug.LogError(
                    $"Edge {name}: From {From.name} cannot connect to {To.name}.",
                    this);
            }
        }
    }
}
