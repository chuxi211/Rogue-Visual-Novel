using StateMachine.Rogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class RogueInitiallizer
{
    public LayerMapLoader LayerMapLoader { get; private set; }
    public RogueInitiallizer(LayerMapLoader layerMapLoader)
    {
        LayerMapLoader = layerMapLoader;
    }
    public void InitContext(RogueContext context)
    {

    }
}