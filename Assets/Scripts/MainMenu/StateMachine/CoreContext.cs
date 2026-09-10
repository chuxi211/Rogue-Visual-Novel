using Audio;
using Running;
using Setting;
using UnityEngine;
using Utility;

public class CoreContext
{

    public SettingService SettingService { get; private set; }
    public AssetBundleManager AssetBundleManager { get; private set; }
    public AudioSystem AudioSystem { get; private set; }
    public AudioDefinitionRepository AudioDefinitionRepository { get; private set; }
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
}
