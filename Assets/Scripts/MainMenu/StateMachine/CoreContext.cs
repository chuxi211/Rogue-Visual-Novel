using Audio;
using Input;
using Running;
using Setting;
using System.Reflection;
using UnityEngine;
using Utility;
namespace StateMachine.Core
{

    public class CoreContext
    {

        public SettingService SettingService { get; private set; }
        public AssetBundleManager AssetBundleManager { get; private set; }
        public AudioSystem AudioSystem { get; private set; }
        public AudioDefinitionRepository AudioDefinitionRepository { get; private set; }

        public InputManager InputManager { get; private set; }
        public void BindSettingService(SettingService settingService)
        {
            if (settingService == null)
            {
                Debug.LogError(
                    "CoreContext failed to bind SettingRepository: " +
                    "SettingRepository is null.");
                return;
            }

            SettingService = settingService;
        }

        public void BindABManager(AssetBundleManager assetBundleManager)
        {
            if (assetBundleManager == null)
            {
                Debug.LogError(
                    "CoreContext failed to bind AssetBundleManager: " +
                    "AssetBundleManager is null.");
                return;
            }

            AssetBundleManager = assetBundleManager;
        }
        public void BindAudioSystem(AudioSystem audioSystem)
        {
            if (audioSystem == null)
            {
                Debug.LogError(
                    "CoreContext failed to bind AudioSystem: " +
                    "AudioSystem is null.");
                return;
            }
            AudioSystem = audioSystem;
        }
        public void BindAudioDefinitionRepository(AudioDefinitionRepository audioDefinitionRepository)
        {
            if (audioDefinitionRepository == null)
            {
                Debug.LogError(
                    "CoreContext failed to bind AudioDefinitionRepository: " +
                    "AudioDefinitionRepository is null.");
                return;
            }
            AudioDefinitionRepository = audioDefinitionRepository;
        }

        public void BindInputManager(InputManager inputManager)
        {
            if (inputManager == null)
            {
                Debug.LogError(
                    "CoreContext failed to bind InputManager: " +
                    "InputManager is null.");
                return;
            }

            InputManager = inputManager;
        }
        public void OnExit()
        {
            //foreach(var field in this.GetType().GetFields())
            //{
            //    field.OnExit();
            //如果要用这种方法，需要显式让field property实现interface OnExit
            //我会考虑
            //}
            SettingService?.OnExit();
            AudioSystem?.Dispose();
        }
    }
}
