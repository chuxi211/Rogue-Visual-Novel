using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Utility
{
    public interface IAssetLoader
    {

    }
    public interface IAssetLoader<T>
    {
        T Load(string path,string assetName);
        T AsyncLoad(string path, string assetName);
        bool TryLoad(string path, string assetName, out T result);
        bool TryAsyncLoad(string path, string assetName, out T result);
    }
}