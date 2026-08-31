using Running;
using StateMachine.Core;
using UnityEngine;
using Utility;

public class CoreContext
{

    public SettingRepository SettingRepository { get; private set; }
    public AssetBundleManager AssetBundleManager { get; private set; }
    public void BindSettingRepository(SettingRepository settingRepository)
    {
        SettingRepository = settingRepository;
    }
    public void BindABManager(AssetBundleManager assetBundleManager)
    {
        AssetBundleManager = assetBundleManager;
    }
}
