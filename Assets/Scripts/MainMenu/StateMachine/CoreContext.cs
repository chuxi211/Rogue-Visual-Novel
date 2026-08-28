using Running;
using UnityEngine;

public class CoreContext
{
    private readonly SettingLoader SettingLoader;

    public SettingRepository SettingRepository { get; private set; }
    public SettingInitializer SettingInitializer { get; private set; }

    public CoreContext(SettingLoader settingLoader)
    {
        if (settingLoader == null)
        {
            Debug.LogError(
                "CoreContext initialization failed: " +
                "SettingLoader is null.");
            return;
        }

        SettingLoader = settingLoader;
    }

    public bool Init()
    {
        if (SettingLoader == null)
        {
            Debug.LogError(
                "CoreContext initialization failed: " +
                "SettingLoader is null.");
            return false;
        }

        SettingInitializer = new SettingInitializer(SettingLoader);

        if (!SettingInitializer.TryInitSetting(
                out SettingRepository settingRepository))
        {
            Debug.LogError(
                "CoreContext initialization failed: " +
                "SettingRepository initialization failed.");
            return false;
        }

        SettingRepository = settingRepository;
        return true;
    }
}
