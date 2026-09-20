using Audio;
using UnityEngine;
namespace Utility
{
    public class AudioDefinitionsLoader : IAssetLoader<AudioDefinitionCatalog>
    {
        private AssetBundleManager AssetBundleManager;
        public AudioDefinitionsLoader(AssetBundleManager assetBundleManager)
        {
            AssetBundleManager = assetBundleManager;
        }
        public AudioDefinitionCatalog AsyncLoad(string bundleName, string assetName)
        {
            throw new System.NotImplementedException();
        }

        public AudioDefinitionCatalog Load(string bundleName, string assetName)
        {
            throw new System.NotImplementedException();
        }

        public bool TryAsyncLoad(string bundleName, string assetName, out AudioDefinitionCatalog result)
        {
            throw new System.NotImplementedException();
        }

        public bool TryLoad(string bundleName, string assetName, out AudioDefinitionCatalog result)
        {
            result = null;

            if (string.IsNullOrEmpty(bundleName))
            {
                Debug.LogError("Bundle name is null or empty.");
                return false;
            }

            if (string.IsNullOrEmpty(assetName))
            {
                Debug.LogError("Asset name is null or empty.");
                return false;
            }

            if (AssetBundleManager == null)
            {
                Debug.LogError(
                    "LayerMapLoader failed to load: " +
                    "AssetBundleManager is null.");
                return false;
            }
            if (!AssetBundleManager.TryGetBundleWithDependencies(bundleName, out AssetBundle bundle))
            {
                Debug.LogError($"Failed to load bundle '{bundleName}' with dependencies.");
                return false;
            }
            result = bundle.LoadAsset<AudioDefinitionCatalog>(assetName);
            if (result == null)
            {
                Debug.LogError($"Failed to load asset '{assetName}' from bundle '{bundleName}'.");
                return false;
            }
            return true;
        }
    }
}