using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Config;
namespace Utility
{
    public class PrefabLoader : IAssetLoader<GameObject>
    {
        public GameObject AsyncLoad(string path, string assetName)
        {
            throw new System.NotImplementedException();
        }

        public GameObject Load(string path, string assetName)
        {
            AssetBundle bundle = AssetBundle.LoadFromFile(path);
            if (bundle == null)
            {
                Debug.LogError($"Fail to load AssetBundle:{path}");
                return null;
            }
            GameObject prefab= bundle.LoadAsset<GameObject>(assetName);
            if(prefab == null)
            {
                Debug.LogError($"Fail to load Prefab:{assetName} from {path}");
                return null;
            }
            bundle.Unload(false);
            return prefab;
        }

        public bool TryAsyncLoad(string path, string assetName, out GameObject result)
        {
            throw new System.NotImplementedException();
        }

        public bool TryLoad(string path, string assetName, out GameObject result)
        {
            AssetBundle bundle = AssetBundle.LoadFromFile(path);
            if(bundle == null)
            {
                Debug.LogError($"Fail to load AssetBundle:{path}");
                result = null;
                return false;
            }
            GameObject prefab=bundle.LoadAsset<GameObject>(assetName);
            if(prefab == null)
            {
                Debug.LogError($"Fail to load Prefab:{assetName} from {path}");
                result=null;
                return false;
            }
            result = prefab;
            bundle.Unload(false);
            return true;
        }
    }
}