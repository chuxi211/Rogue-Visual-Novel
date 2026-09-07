
using Audio;
using Running;
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
        public CGPlayer CGPlayer { get; private set; }
        public CGLoader CGLoader { get; private set; }
        public List<UIButton> Buttons { get; private set; }
        public CoreInitilaizer(SettingLoader settingLoader,CGPlayer cgPlayer,List<UIButton> buttons)
        {
            SettingLoader = settingLoader;
            CGPlayer = cgPlayer;
            Buttons = buttons;
        }
        public bool InitContext(CoreContext coreContext)
        {
            if (coreContext == null)
            {
                Debug.LogError(
                    "CoreContext initialization failed: " +
                    "CoreContext is null.");
                return false;
            }
            // Initialize SettingRepository
            if (!InitSettingRepository(out SettingRepository settingRepository))
            {
                return false;
            }
            //Get AssetBundlesRoot from SettingRepository
            string root = settingRepository.SettingData.OtherSetting.AssetBundlesRoot;

            if (string.IsNullOrEmpty(root))
            {
                Debug.LogError(
                    "CoreContext initialization failed: " +
                    "AssetBundlesRoot is null or empty.");
                return false;
            }
            // Initialize AssetBundleManager
            if (!InitAssetBundleManager(root, out AssetBundleManager assetBundleManager))
            {
                return false;
            }

            if(!InitCGPlayer())
            {
                return false;
            }
            if(!InitAudioSystem(out AudioSystem audioSystem))
            {
                return false;
            }
            InitButtons(settingRepository.SettingData.OtherSetting.ButtonSE,audioSystem);
            coreContext.BindSettingRepository(settingRepository);
            coreContext.BindABManager(assetBundleManager);
            return true;
        }
        public bool InitAudioSystem(out  AudioSystem audioSystem)
        {
            audioSystem=new AudioSystem();
            return true;
        }

        public bool InitAssetBundleManager(string root, out AssetBundleManager assetBundleManager)
        {
            assetBundleManager = null;

            if (string.IsNullOrEmpty(root))
            {
                Debug.LogError(
                    "AssetBundleManager initialization failed: " +
                    "AssetBundlesRoot is null or empty.");
                return false;
            }

            string platform = null;

            switch (Application.platform)
            {
                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.WindowsEditor:
                    platform = "Windows";
                    break;
                default:
                    Debug.LogError(
                        "CoreContext initialization failed: " +
                        "Unsupported platform: " + Application.platform);
                    assetBundleManager = null;
                    return false;
            }

            AssetBundleManager manager = new AssetBundleManager(root, platform);

            if (!manager.TryInitialize())
            {
                Debug.LogError(
                    "CoreContext initialization failed: " +
                    "AssetBundleManager initialization failed.");
                return false;
            }

            AssetBundleManager = manager;
            assetBundleManager = manager;
            return true;
        }

        public bool InitSettingRepository(out SettingRepository settingRepository)
        {
            if (SettingLoader == null)
            {
                Debug.LogError(
                    "CoreContext initialization failed: " +
                    "SettingLoader is null.");
                settingRepository = null;
                return false;
            }

            SettingInitializer = new SettingInitializer(SettingLoader);

            if (!SettingInitializer.TryInitSetting(out settingRepository))
            {
                Debug.LogError(
                    "CoreContext initialization failed: " +
                    "SettingRepository initialization failed.");
                settingRepository = null;
                return false;
            }

            return true;
        }
        public bool InitCGPlayer()
        {
            if(CGPlayer == null)
            {
                Debug.LogError("CoreContext initialization failed: CGPlayer is null.");
                return false;
            }
            CGLoader = new CGLoader(AssetBundleManager);
            CGPlayer.InitCGLoader(CGLoader);
            return true;
        }
        public void InitButtons(AudioID audioID,ISoundPlayback soundPlayback)
        {
            if (Buttons == null || Buttons.Count == 0)
            {
                Debug.LogWarning("CoreContext initialization warning: Buttons list is null or empty.");
                return;
            }
            foreach (var button in Buttons)
            {
                if (button == null)
                {
                    Debug.LogWarning("CoreContext initialization warning: A button in the list is null.");
                    continue;
                }
                button.InitSE(audioID,soundPlayback);
            }
        }
    }
}
