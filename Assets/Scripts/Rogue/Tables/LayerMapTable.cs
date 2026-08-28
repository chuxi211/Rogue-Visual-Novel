using Config;
using System.Collections.Generic;
using UnityEngine;
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
    public bool TryGetMap(int layer, out Map configmap)
    {
        if(!LayerMaps.TryGetValue(layer,out LayerMapGroup group))
        {
            configmap = null;
            return false;
        }
        if(group == null)
        {
            configmap = null;
            return false;
        }
        if(group.Variants.Count == 0)
        {
            Debug.LogError($"The Variants of group({layer}) count is 0");
            configmap = null;
            return false;
        }
        configmap = group.Variants[Random.Range(0,group.Variants.Count)];
        return configmap != null;
    }
}