using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ERequestEnterNode
{
    public NodeActor Actor { get; private set; }
    public ERequestEnterNode(NodeActor actor)
    {
        Actor = actor;
    }
}