using Config;
using System.Collections.Generic;
using UnityEngine;

public class LayerMapTable
{
    public Dictionary<int, LayerMapGroup> LayerMaps { get; private set; } = new Dictionary<int, LayerMapGroup>();
    public Dictionary<MapID,Map> Maps { get; private set; } = new Dictionary<MapID, Map>();
    public void Add(int layer, LayerMapGroup group)
    {
        LayerMaps[layer] = group;
    }
    public void Add(MapID mapID, Map map)
    {
        Maps[mapID] = map;
    }
    public bool TryGetMapGroup(int layer, out LayerMapGroup group)
    {
        return LayerMaps.TryGetValue(layer, out group);
    }
    public bool TryGetMap(MapID mapID, out Map map)
    {
        return Maps.TryGetValue(mapID, out map);
    }
    public bool TryGetMap(int layer, out Map configmap)
    {
        if (!LayerMaps.TryGetValue(layer, out LayerMapGroup group))
        {
            configmap = null;
            return false;
        }
        if (group == null)
        {
            configmap = null;
            return false;
        }
        if (group.Variants == null || group.Variants.Count == 0)
        {
            Debug.LogError($"The Variants of group({layer}) count is 0");
            configmap = null;
            return false;
        }
        configmap = group.Variants[Random.Range(0, group.Variants.Count)];
        return configmap != null;
    }
}
