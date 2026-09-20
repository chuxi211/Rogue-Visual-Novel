using Config;
using System;
using UnityEngine;

namespace Utility
{
    public class LayerMapLoader : IAssetLoader<LayerMapGroup>
    {
        private readonly AssetBundleManager AssetBundleManager;

        public LayerMapLoader(AssetBundleManager assetBundleManager)
        {
            if (assetBundleManager == null)
            {
                Debug.LogError(
                    "LayerMapLoader initialization failed: " +
                    "AssetBundleManager is null.");
            }

            AssetBundleManager = assetBundleManager;
        }

        public bool TryLoad(string bundleName, string assetName, out LayerMapGroup result)
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

            if (bundle == null)
            {
                Debug.LogError(
                    "LayerMapLoader failed to load: " +
                    $"Bundle '{bundleName}' is null.");
                return false;
            }

            result = bundle.LoadAsset<LayerMapGroup>(assetName);

            if (result == null)
            {
                Debug.LogError(
                    $"Failed to load asset '{assetName}' from bundle '{bundleName}'.");
                return false;
            }

            return true;
        }

        public LayerMapGroup Load(string bundleName, string assetName)
        {
            TryLoad(bundleName, assetName, out LayerMapGroup result);
            return result;
        }

        public LayerMapGroup AsyncLoad(string bundleName, string assetName)
        {
            throw new NotImplementedException();
        }

        public bool TryAsyncLoad(string bundleName, string assetName, out LayerMapGroup result)
        {
            throw new NotImplementedException();
        }
    }
}
