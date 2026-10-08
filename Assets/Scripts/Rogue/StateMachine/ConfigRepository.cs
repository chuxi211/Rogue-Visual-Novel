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
    public GetMapCode TryGetLayerMapGroup(int layer, out LayerMapGroup layerMap)
    {
        if (MapConfigTable.TryGetMapGroup(layer, out layerMap)) return GetMapCode.SucceededToGet;
        if (!LayerMapLoader.TryLoad(LayerToBundleName(layer), LayerToAssetName(layer), out layerMap)) return GetMapCode.FailedToGet;
        MapConfigTable.Add(layer, layerMap); return GetMapCode.SucceededToGet;
    }
    public GetMapCode TryGetMap(MapID mapID, out Config.Map map)
    {
        map = null;
        if (mapID == null)
        {
            return GetMapCode.InvaildMapID;
        }
        if (MapConfigTable.TryGetMap(mapID, out map))
        {
            if (map.ID == mapID)
            {
                return GetMapCode.SucceededToGet;
            }
            else
            {
                return GetMapCode.FailedToGet;
            }
        }
        return GetMapCode.FailedToGet;
        //if (!LayerMapLoader.TryLoad(LayerToBundleName(mapID.Layer), LayerToAssetName(mapID.Layer), out LayerMapGroup layerMapGroup))
        //{
        //    return GetMapCode.FailedToGet;
        //}
        //MapConfigTable.Add(mapID.Layer, layerMapGroup);
        //MapConfigTable.TryGetMap(mapID.Layer, out map);
        //if (map == null || map.ID != mapID)
        //{
        //    return GetMapCode.FailedToGet;
        //}
        //return GetMapCode.SucceededToGet;
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

        if (!LayerMapLoader.TryLoad(LayerToBundleName(layer), LayerToAssetName(layer), out LayerMapGroup layerMapGroup))
        {
            return GetMapCode.FailedToGet;
        }

        MapConfigTable.Add(layer, layerMapGroup);
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