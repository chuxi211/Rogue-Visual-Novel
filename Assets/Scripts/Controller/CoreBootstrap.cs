using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.Core;
using Setting;
using Setting.Config;
using Utility;
using Input;

public class CoreBootstrap : MonoBehaviour
{
    public static CoreBootstrap Instance { get; private set; }
    public CoreStateMachine StateMachine { get; private set; }
    public DefaultSettingConfig DefaultSettingConfig;
    public CanvasGroup Menu;
    public CanvasGroup GalleryCnvsGrp;
    public CGPlayer CGPlayer;
    public Transform AudioPlayers;
    public List<UIButton> Buttons;
    public InputCollector InputCollector;
    public InputManager InputManager;
    private void Awake()
    {
        if(Instance != null&& Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(Instance);
        DontDestroyOnLoad(AudioPlayers);
        InitCoreStateMachine();
        LogApplicationInfo();
    }
    private void OnDestroy()
    {
        if (StateMachine == null)
        {
            return;
        }

        StateMachine.SwitchState<ExitState>();
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
        CoreInitilaizer coreInitilaizer = new CoreInitilaizer(
            settingLoader,
            CGPlayer,
            Buttons,
            AudioPlayers,
            InputCollector,
            InputManager);
        StateMachine = new CoreStateMachine();
        StateMachine.RegisterState(new InitState(StateMachine,coreInitilaizer));
        StateMachine.RegisterState(new ExitState(StateMachine));
        StateMachine.RegisterState(new MainMenuState(StateMachine,Menu));
        StateMachine.RegisterState(new RogueModeState(StateMachine));
        StateMachine.RegisterState(new GalleryState(StateMachine,GalleryCnvsGrp,CGPlayer));
        StateMachine.SwitchState<InitState>();
    }
    public void LogApplicationInfo()
    {
        Debug.Log($"DataPath: {Application.dataPath}");
        Debug.Log($"PersistentDataPath:{Application.persistentDataPath}");
    }
}
