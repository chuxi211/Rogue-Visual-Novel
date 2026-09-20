using System.Collections.Generic;

namespace Running
{
    public class Map
    {
        public List<Node> Nodes { get; private set; } = new List<Node>();
        private readonly List<Edge> Edges = new List<Edge>();
        public bool TryAddEdge(Edge edge)
        {
            if (!IsValidEdge(edge)) return false;
            if (Edges.Contains(edge)) return false;
            Edges.Add(edge);
            edge.From.AddOutgoingEdge(edge);
            edge.To.AddIncomingEdge(edge);
            return true;
        }
        private bool IsValidEdge(Edge edge)
        {
            if (edge == null) return false;
            if (edge.From == null) return false;
            if (edge.To == null) return false;
            if (edge.From == edge.To) return false;
            if (!edge.IsValid) return false;
            return true;
        }
        public IReadOnlyList<Edge> GetEdges()
        {
            return Edges;
        }
    }
}