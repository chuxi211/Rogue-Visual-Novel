using UnityEngine;

namespace Running
{
    public class Edge
    {
        public Node From { get; private set; }
        public Node To { get; private set; }
        public Edge(Node from, Node to)
        {
            From = from;
            To = to;
        }
        public bool IsValid
        {
            get
            {
                if (From == null || To == null) return false;
                if (From == To) return false;
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
        }
    }
}