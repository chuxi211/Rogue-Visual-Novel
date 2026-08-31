using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace CGLoader
{
    public class CGLoader : IAssetLoader<CGList>
    {
        private AssetBundleManifest Manifest;
        private AssetBundleManager AssetBundleManager;
        public CGList AsyncLoad(string path, string assetName)
        {
            throw new System.NotImplementedException();
        }

        public CGList Load(string path, string assetName)
        {
            throw new System.NotImplementedException();
        }

        public bool TryAsyncLoad(string path, string assetName, out CGList result)
        {
            throw new System.NotImplementedException();
        }

        public bool TryLoad(string path, string assetName, out CGList result)
        {
            result = null;
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }
            if (string.IsNullOrEmpty(assetName))
            {
                return false;
            }
        }
        private bool TryGetAllDependences(string assetName,out AssetBundle targetBundle)
        {
            targetBundle = null;
            string[] dependences=Manifest.GetAllDependencies(assetName);
            foreach(var dependen in dependences)
            {
                
            }
        }
    }
}