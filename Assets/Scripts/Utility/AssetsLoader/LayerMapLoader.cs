using Config;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Utility
{
    public class LayerMapLoader : IAssetLoader<LayerMapGroup>
    {
        private readonly string BundleRootPath;
        private readonly AssetBundleManifest Manifest;

        private readonly Dictionary<string, AssetBundle> LoadedBundles = new();

        public LayerMapLoader(string bundleRootPath,AssetBundleManifest manifest)
        {
            BundleRootPath = bundleRootPath;
            Manifest = manifest;
        }

        public bool TryLoad(string bundleName,string assetName,out LayerMapGroup result)
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

            if (!TryLoadBundleWithDependencies(bundleName, out AssetBundle bundle))
            {
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

        private bool TryLoadBundleWithDependencies(string bundleName,out AssetBundle targetBundle)
        {
            targetBundle = null;

            string[] dependencies = Manifest.GetAllDependencies(bundleName);

            foreach (string dependencyName in dependencies)
            {
                if (!TryLoadBundle(dependencyName, out _))
                {
                    Debug.LogError(
                        $"Failed to load dependency '{dependencyName}' for '{bundleName}'.");
                    return false;
                }
            }

            return TryLoadBundle(bundleName, out targetBundle);
        }

        private bool TryLoadBundle(string bundleName,out AssetBundle bundle)
        {
            if (LoadedBundles.TryGetValue(bundleName, out bundle))
            {
                return true;
            }

            string path = Path.Combine(BundleRootPath, bundleName);

            bundle = AssetBundle.LoadFromFile(path);

            if (bundle == null)
            {
                Debug.LogError($"Failed to load AssetBundle: {path}");
                return false;
            }

            LoadedBundles.Add(bundleName, bundle);
            return true;
        }

        public LayerMapGroup Load(string path, string assetName)
        {
            TryLoad(path, assetName, out LayerMapGroup result);
            return result;
        }

        public LayerMapGroup AsyncLoad(string path, string assetName)
        {
            throw new NotImplementedException();
        }

        public bool TryAsyncLoad(string path,string assetName,out LayerMapGroup result)
        {
            throw new NotImplementedException();
        }
    }
}