using Config;
using System.Collections.Generic;
using Utility;

public class LayerMapTable
{
    public Dictionary<int,LayerMapGroup> LayerMaps { get;private set; }=new Dictionary<int,LayerMapGroup>();
    public void Add(int layer, LayerMapGroup group)
    {
        LayerMaps[layer]=group;
    }
    public bool TryGetMapGroup(int layer, out LayerMapGroup group)
    {
        return LayerMaps.TryGetValue(layer, out group);
    }
}