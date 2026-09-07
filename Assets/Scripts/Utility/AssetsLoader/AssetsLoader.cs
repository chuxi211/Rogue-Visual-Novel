namespace Utility
{
    public interface IAssetLoader
    {

    }
    public interface IAssetLoader<T>
    {
        T Load(string bundleName,string assetName);
        T AsyncLoad(string bundleName, string assetName);
        bool TryLoad(string bundleName, string assetName, out T result);
        bool TryAsyncLoad(string bundleName, string assetName, out T result);
    }
}
