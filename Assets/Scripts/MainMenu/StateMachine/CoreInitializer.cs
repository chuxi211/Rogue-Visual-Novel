using Running;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace StateMachine.Core
{
    public class CoreInitilaizer
    {
        private readonly SettingLoader SettingLoader;
        public SettingInitializer SettingInitializer { get; private set; }
        public AssetBundleManager AssetBundleManager { get; private set; }
        public CoreInitilaizer(SettingLoader settingLoader)
        {
            SettingLoader = settingLoader;
        }
        public void InitContext(CoreContext coreContext)
        {
            if(InitSettingRepository(out SettingRepository settingrepository))
            {
                coreContext.BindSettingRepository(settingrepository);
            }
            if(InitAssetBundleManager(out AssetBundleManager assetbundlemanager))
            {
                coreContext.BindABManager(assetbundlemanager);
            }
        }
        public bool InitAssetBundleManager(out AssetBundleManager assetBundleManager)
        {
            AssetBundleManager=new AssetBundleManager();
            assetBundleManager = AssetBundleManager;
            return true;
        }
        public bool InitSettingRepository(out SettingRepository settingrepository)
        {
            if (SettingLoader == null)
            {
                Debug.LogError(
                    "CoreContext initialization failed: " +
                    "SettingLoader is null.");
                settingrepository = null;
                return false;
            }

            SettingInitializer = new SettingInitializer(SettingLoader);

            if (!SettingInitializer.TryInitSetting(
                    out SettingRepository settingRepository))
            {
                Debug.LogError(
                    "CoreContext initialization failed: " +
                    "SettingRepository initialization failed.");
                settingrepository = null;
                return false;
            }

            settingrepository = settingRepository;
            return true;
        }
    }
}