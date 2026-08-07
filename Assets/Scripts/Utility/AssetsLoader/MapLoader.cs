using Config;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public class MapLoader : IAssetLoader<Map>
    {
        public Map AsyncLoad(string path, string assetName)
        {
            throw new System.NotImplementedException();
        }

        public Map Load(string path, string assetName)
        {
            AssetBundle bundle=AssetBundle.LoadFromFile(path);
            if (bundle == null)
            {
                Debug.LogError($"Fail to load AssetBundle:{path}");
                return null;
            }
            Map map = bundle.LoadAsset<Map>(assetName);
            if (map == null)
            {
                Debug.Log($"Fail to load Map:{assetName}");
                return null;
            }
            bundle.Unload(false);
            return map;
        }

        public bool TryAsyncLoad(string path, string assetName, out Map result)
        {
            throw new System.NotImplementedException();
        }

        public bool TryLoad(string path, string assetName, out Map result)
        {
            AssetBundle bundle = AssetBundle.LoadFromFile(path);
            if (bundle == null)
            {
                Debug.LogError($"Fail to load AssetBundle:{path}");
                result = null;
                return false;
            }
            Map map = bundle.LoadAsset<Map>(assetName);
            if (map == null)
            {
                Debug.Log($"Fail to load Map:{assetName}");
                result=null;
                return false;
            }
            result = map;
            bundle.Unload(false);
            return true;
        }
    }
}