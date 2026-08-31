using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.Core;
using Config;
using Running;

public class CoreBootstrap : MonoBehaviour
{
    public static CoreBootstrap Instance { get; private set; }
    public CoreStateMachine StateMachine { get; private set; }
    public DefaultSettingConfig DefaultSettingConfig;
    public CanvasGroup Menu;
    public CanvasGroup CGCanvas;
    public CGPlayer CGPlayer;
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
        CoreInitilaizer coreInitilaizer = new CoreInitilaizer(settingLoader);
        StateMachine = new CoreStateMachine();
        StateMachine.RegisterState(new InitState(StateMachine,coreInitilaizer));
        StateMachine.RegisterState(new MainMenuState(StateMachine,Menu));
        StateMachine.RegisterState(new RogueModeState(StateMachine));
        StateMachine.RegisterState(new GalleryState(StateMachine,CGCanvas,CGPlayer));
        StateMachine.SwitchState<InitState>();
    }
}
