using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.Core;
using Config;
using Running;

public class CoreController : MonoBehaviour
{
    public static CoreController Instance { get; private set; }
    public CoreStateMachine StateMachine { get; private set; }
    public DefaultSettingConfig DefaultSettingConfig;
    private void Awake()
    {
        if(Instance != null&& Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(Instance);
        InitCoreStateMachine();
    }
    private void InitCoreStateMachine()
    {
        if (DefaultSettingConfig == null)
        {
            Debug.LogError(
                "CoreController initialization failed: " +
                "DefaultSettingConfig is null.");
            return;
        }

        SettingLoader settingLoader = new SettingLoader(DefaultSettingConfig);
        CoreContext coreContext = new CoreContext(settingLoader);
        StateMachine = new CoreStateMachine(coreContext);
        StateMachine.RegisterState(new InitState(StateMachine));
        StateMachine.RegisterState(new MainMenuState(StateMachine));
        StateMachine.RegisterState(new RogueModeState(StateMachine));
        StateMachine.SwitchState<InitState>();
    }
}
