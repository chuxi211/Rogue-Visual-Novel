using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Running
{
    public class LayerProgression
    {
        public int CurrentLayer { get; private set; }
        public void SetLayer(int layer)
        {
            CurrentLayer = layer;
        }
        public void AddLayer()
        {
            CurrentLayer++;
        }
    }
}