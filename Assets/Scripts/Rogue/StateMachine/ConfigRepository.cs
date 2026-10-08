using Config;
using System;
using Utility;
using UnityEngine;

public class ConfigRepository
{
    //目前只有Map
    //之后可以扩展为所有Config
    public LayerMapTable MapConfigTable { get; private set; } = new LayerMapTable();
    private IAssetLoader<LayerMapGroup> LayerMapLoader;
    public ConfigRepository(IAssetLoader<LayerMapGroup> layerMapLoader)
    {
        LayerMapLoader = layerMapLoader;
    }
    public GetMapCode TryGetLayerMapGroup(int layer, out LayerMapGroup layerMap)
    {
        if (MapConfigTable.TryGetMapGroup(layer, out layerMap)) return GetMapCode.SucceededToGet;
        try
        {
            if (!LayerMapLoader.TryLoad(LayerToBundleName(layer), LayerToAssetName(layer), out layerMap))
                return GetMapCode.FailedToGet;
        }
        catch (Exception exception)
        {
            Debug.LogException(exception);
            layerMap = null;
            return GetMapCode.FailedToGet;
        }
        MapConfigTable.Add(layer, layerMap); return GetMapCode.SucceededToGet;
    }
    public GetMapCode TryGetMap(MapID mapID, out Config.Map map)
    {
        map = null;
        if (!mapID.IsValid)
        {
            return GetMapCode.InvaildMapID;
        }
        if (MapConfigTable.TryGetMap(mapID, out map) && map != null)
        {
            return GetMapCode.SucceededToGet;
        }

        if (TryGetLayerMapGroup(mapID.LayerNumber, out LayerMapGroup group) != GetMapCode.SucceededToGet ||
            group == null || group.Variants == null)
        {
            return GetMapCode.FailedToGet;
        }
        foreach (Map variant in group.Variants)
        {
            if (variant != null && variant.ID == mapID)
            {
                map = variant;
                MapConfigTable.Add(mapID, map);
                return GetMapCode.SucceededToGet;
            }
        }
        return GetMapCode.FailedToGet;
    }
    public GetMapCode TryGetMap(int layer,int maxlayer, out Map map)
    {
        map = null;
        if(layer < 0 || maxlayer < 0)
        {
            return GetMapCode.InvaildLayer;
        }
        if (layer > maxlayer)
        {
            return GetMapCode.Ended;
        }
        if (MapConfigTable.TryGetMap(layer, out map))
        {
            return GetMapCode.SucceededToGet;
        }

        if (TryGetLayerMapGroup(layer, out LayerMapGroup layerMapGroup) != GetMapCode.SucceededToGet ||
            layerMapGroup == null)
        {
            return GetMapCode.FailedToGet;
        }
        MapConfigTable.TryGetMap(layer, out map);
        if(map == null)
        {
            return GetMapCode.FailedToGet;
        }
        return GetMapCode.SucceededToGet;
    }
    private string LayerToAssetName(int layer)
    {
        return $"LayerMap_{layer}";
    }
    private string LayerToBundleName(int layer)
    {
        return $"layermap_{layer}";
    }
}
public enum GetMapCode
{
    SucceededToGet,
    FailedToGet,
    InvaildMapID,
    InvaildLayer,
    Ended
}
