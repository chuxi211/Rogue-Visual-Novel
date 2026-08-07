using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using Config;

public class ConfigRepository
{
    public LayerMapTable MapConfigTable { get; private set; }=new LayerMapTable();
    private IAssetLoader<LayerMapGroup> LayerMapLoader;
    public ConfigRepository(IAssetLoader<LayerMapGroup> layerMapLoader)
    {
        LayerMapLoader = layerMapLoader;
    }
    public bool TryGetLayerMap(int layer,out LayerMapGroup layerMap)
    {
        if(MapConfigTable.TryGetMapGroup(layer,out layerMap)) return true;
        if(!LayerMapLoader.TryLoad(LayerToBundleName(layer),LayerToAssetName(layer),out layerMap)) return false;
        MapConfigTable.Add(layer, layerMap); return true;
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