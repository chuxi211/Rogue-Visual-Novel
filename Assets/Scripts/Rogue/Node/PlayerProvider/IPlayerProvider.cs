namespace Running
{
    //为防止各个Component随意FindObject，GetPlayer，或从PrefabLoader直接加载
    //抽象出一个统一的接口，为Component提供获取Player的能力

    //该接口提供统一的父类抽象
    public interface IPlayerProvider
    {

    }
    //该接口为不同Player提供通用泛型接口
    // 泛型接口提供类型安全的Player获取能力，
    // 避免调用方通过object或Component获取后再进行强制转换。
    public interface IPlayerProvider<TPlayer> : IPlayerProvider where TPlayer : class
    {
        TPlayer GetPlayer();
    }

    //若舍弃IPlayerProvider，则各泛型之间没有统一的非泛型父类型，导致无法统一管理
    //若舍弃IPlayerProvider<TPlayer>，则无法为不同Player提供统一的接口
}