using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public class AudioLoader:IAssetLoader<AudioClip>
    {
        private AssetBundleManager AssetBundleManager;
        public AudioLoader(AssetBundleManager assetBundleManager)
        {
            AssetBundleManager = assetBundleManager;
        }
        public AudioClip AsyncLoad(string bundleName, string assetName)
        {
            throw new System.NotImplementedException();
        }

        public AudioClip Load(string bundleName, string assetName)
        {
            if (AssetBundleManager == null)
            {
                Debug.LogError(
                    "AudioLoader failed to load: " +
                    "AssetBundleManager is null.");
                return null;
            }
            if (AssetBundleManager.TryGetBundleWithDependencies(bundleName, out AssetBundle bundle))
            {
                if (bundle == null)
                {
                    Debug.LogError(
                        "AudioLoader failed to load: " +
                        $"Bundle '{bundleName}' is null.");
                    return null;
                }
                AudioClip clip = bundle.LoadAsset<AudioClip>(assetName);
                if (clip == null)
                {
                    Debug.LogError(
                        "AudioLoader failed to load: " +
                        $"Asset '{assetName}' is null.");
                    return null;
                }
                return clip;
            }
            else
            {
                Debug.LogError($"Failed to load bundle '{bundleName}' with dependencies.");
                return null;
            }
        }

        public bool TryAsyncLoad(string bundleName, string assetName, out AudioClip result)
        {
            throw new System.NotImplementedException();
        }

        public bool TryLoad(string bundleName, string assetName, out AudioClip result)
        {
            throw new System.NotImplementedException();
        }
    }
}