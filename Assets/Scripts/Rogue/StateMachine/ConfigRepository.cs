using Config;
using Utility;

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
    public bool TryGetLayerMapGroup(int layer, out LayerMapGroup layerMap)
    {
        if (MapConfigTable.TryGetMapGroup(layer, out layerMap)) return true;
        if (!LayerMapLoader.TryLoad(LayerToBundleName(layer), LayerToAssetName(layer), out layerMap)) return false;
        MapConfigTable.Add(layer, layerMap); return true;
    }
    public bool TryGetMap(int layer, out Map map)
    {
        if (MapConfigTable.TryGetMap(layer, out map))
        {
            return true;
        }

        if (!LayerMapLoader.TryLoad(LayerToBundleName(layer), LayerToAssetName(layer), out LayerMapGroup layerMapGroup))
        {
            map = null;
            return false;
        }

        MapConfigTable.Add(layer, layerMapGroup);
        return MapConfigTable.TryGetMap(layer, out map);
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