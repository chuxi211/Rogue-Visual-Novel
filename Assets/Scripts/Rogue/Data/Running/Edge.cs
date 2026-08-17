using System.Collections;
using System.Collections.Generic;
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
    }
}