using UnityEngine;
using MainMenu;

namespace Utility
{
    //由于先做了Rogue部分，导致CoreInitializer和RogueInitiallizer的初始化方式不一致
    //导致CGLoader和RogueMapLoader的初始化方式也不一致
    //对不起，下次会统一这部分
    public class CGLoader : IAssetLoader<CGList>
    {
        private AssetBundleManager AssetBundleManager;
        public CGLoader(AssetBundleManager assetBundleManager)
        {
            AssetBundleManager = assetBundleManager;
        }

        public CGList AsyncLoad(string bundleName, string assetName)
        {
            throw new System.NotImplementedException();
        }

        public CGList Load(string bundleName, string assetName)
        {
            throw new System.NotImplementedException();
        }

        public bool TryAsyncLoad(string bundleName, string assetName, out CGList result)
        {
            throw new System.NotImplementedException();
        }

        public bool TryLoad(string bundleName, string assetName, out CGList result)
        {
            result = null;
            if (string.IsNullOrEmpty(bundleName))
            {
                return false;
            }
            if (string.IsNullOrEmpty(assetName))
            {
                return false;
            }
            AssetBundleManager.TryGetBundleWithDependencies(bundleName, out AssetBundle bundle);
            if (bundle == null)
            {
                return false;
            }
            result = bundle.LoadAsset<CGList>(assetName);
            if (result == null)
            {
                return false;
            }
            return true;
        }
    }
}