using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Utility
{
    public class AssetBundleManager
    {
        private AssetBundleManifest Manifest;
        private readonly Dictionary<string, AssetBundle> LoadedBundles = new Dictionary<string, AssetBundle>();
        private readonly string AssetBundlesRoot;
        private readonly string PlatformRoot;

        public AssetBundleManager(string root, string platformRoot)
        {
            if (string.IsNullOrEmpty(root))
            {
                Debug.LogError(
                    "AssetBundleManager initialization failed: " +
                    "AssetBundlesRoot is null or empty.");
            }

            if (string.IsNullOrEmpty(platformRoot))
            {
                Debug.LogError(
                    "AssetBundleManager initialization failed: " +
                    "PlatformRoot is null or empty.");
            }

            AssetBundlesRoot = root;
            PlatformRoot = platformRoot;
        }

        public bool TryInitialize()
        {
            if (Manifest != null)
            {
                return true;
            }

            if (!IsPathConfigValid())
            {
                return false;
            }

            string manifestPath = GetBundleManifestPath();

            if (!File.Exists(manifestPath))
            {
                Debug.LogError(
                    "AssetBundleManager initialization failed: " +
                    $"Manifest bundle does not exist at '{manifestPath}'.");
                return false;
            }

            AssetBundle rootBundle = AssetBundle.LoadFromFile(manifestPath);

            if (rootBundle == null)
            {
                Debug.LogError(
                    "AssetBundleManager initialization failed: " +
                    $"Failed to load manifest bundle from '{manifestPath}'.");
                return false;
            }

            try
            {
                Manifest = rootBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");

                if (Manifest == null)
                {
                    Debug.LogError(
                        "AssetBundleManager initialization failed: " +
                        $"AssetBundleManifest is missing from '{manifestPath}'.");
                    return false;
                }

                return true;
            }
            finally
            {
                rootBundle.Unload(false);
            }
        }

        public string GetAssetBundlesRootDirectory()
        {
            return Path.Combine(Application.streamingAssetsPath, AssetBundlesRoot);
        }

        public string GetBundleManifestPath()
        {
            return Path.Combine(GetAssetBundlesRootDirectory(), PlatformRoot, PlatformRoot);
        }

        public string GetFullPath(string bundleName)
        {
            return Path.Combine(GetAssetBundlesRootDirectory(), PlatformRoot, bundleName);
        }

        public bool TryGetBundleWithDependencies(string bundleName, out AssetBundle targetBundle)
        {
            targetBundle = null;

            if (string.IsNullOrEmpty(bundleName))
            {
                Debug.LogError(
                    "AssetBundleManager failed to load bundle: " +
                    "Bundle name is null or empty.");
                return false;
            }

            if (Manifest == null)
            {
                Debug.LogError(
                    "AssetBundleManager failed to load bundle: " +
                    "AssetBundleManifest is null. TryInitialize must succeed before loading bundles.");
                return false;
            }

            if (!TryLoadBundle(bundleName, out targetBundle))
            {
                return false;
            }

            string[] dependencies = Manifest.GetAllDependencies(bundleName);

            foreach (string dependencyName in dependencies)
            {
                if (!TryLoadBundle(dependencyName, out AssetBundle dependencyBundle))
                {
                    Debug.LogError(
                        "AssetBundleManager failed to load dependency: " +
                        $"'{dependencyName}' required by '{bundleName}'.");
                    targetBundle = null;
                    return false;
                }
            }

            return true;
        }

        //除非能明确当前Bundle没有任何依赖，否则优先调用TryGetBundleWithDependencies。
        public bool TryLoadBundle(string bundleName, out AssetBundle targetBundle)
        {
            targetBundle = null;

            if (string.IsNullOrEmpty(bundleName))
            {
                Debug.LogError(
                    "AssetBundleManager failed to load bundle: " +
                    "Bundle name is null or empty.");
                return false;
            }

            if (!IsPathConfigValid())
            {
                return false;
            }

            if (LoadedBundles.TryGetValue(bundleName, out AssetBundle bundle))
            {
                if (bundle != null)
                {
                    targetBundle = bundle;
                    return true;
                }

                Debug.LogError(
                    "AssetBundleManager found an invalid cached bundle: " +
                    $"'{bundleName}'. The invalid entry will be reloaded.");
                LoadedBundles.Remove(bundleName);
            }

            string bundlePath = GetFullPath(bundleName);

            if (!File.Exists(bundlePath))
            {
                Debug.LogError(
                    "AssetBundleManager failed to load bundle: " +
                    $"Bundle does not exist at '{bundlePath}'.");
                return false;
            }

            bundle = AssetBundle.LoadFromFile(bundlePath);

            if (bundle == null)
            {
                Debug.LogError(
                    "AssetBundleManager failed to load bundle: " +
                    $"AssetBundle.LoadFromFile returned null for '{bundlePath}'.");
                return false;
            }

            targetBundle = bundle;
            LoadedBundles.Add(bundleName, bundle);
            return true;
        }

        private bool IsPathConfigValid()
        {
            if (string.IsNullOrEmpty(AssetBundlesRoot))
            {
                Debug.LogError(
                    "AssetBundleManager path configuration is invalid: " +
                    "AssetBundlesRoot is null or empty.");
                return false;
            }

            if (string.IsNullOrEmpty(PlatformRoot))
            {
                Debug.LogError(
                    "AssetBundleManager path configuration is invalid: " +
                    "PlatformRoot is null or empty.");
                return false;
            }

            return true;
        }
    }
}
