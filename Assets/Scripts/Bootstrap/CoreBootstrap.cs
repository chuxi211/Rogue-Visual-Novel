using Input;
using MainMenu.Input;
using Setting;
using Setting.Config;
using StateMachine.Core;
using System.Collections.Generic;
using UnityEngine;

public class CoreBootstrap : MonoBehaviour
{
    public static CoreBootstrap Instance { get; private set; }
    public CoreStateMachine StateMachine { get; private set; }
    public DefaultSettingConfig DefaultSettingConfig;
    public CanvasGroup Menu;
    public CanvasGroup GalleryCnvsGrp;
    public CanvasGroup SettingCnvsGrp;
    public CGPlayer CGPlayer;
    public Transform AudioPlayers;
    public List<MainMenuInput> MainMenuInputs;
    public List<SettingInput> SettingInputs;

    public Transform SettingArea;
    public AudioArea AudioArea;
    public DisplayArea DisplayArea;
    public InputArea InputArea;
    public TextArea TextArea;

    public InputCollector InputCollector;
    public InputManager InputManager;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(Instance);
        DontDestroyOnLoad(AudioPlayers);
        DontDestroyOnLoad(SettingArea);
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
            MainMenuInputs,
            SettingInputs,
            AudioPlayers,
            InputCollector,
            InputManager,
            AudioArea,
            DisplayArea,
            InputArea,
            TextArea);
        StateMachine = new CoreStateMachine();
        StateMachine.RegisterState(new InitState(StateMachine, coreInitilaizer));
        StateMachine.RegisterState(new ExitState(StateMachine));
        StateMachine.RegisterState(new StateMachine.Core.MainMenuState(StateMachine, Menu));
        StateMachine.RegisterState(new RogueModeState(StateMachine));
        StateMachine.RegisterState(new GalleryState(StateMachine, GalleryCnvsGrp, CGPlayer));
        StateMachine.RegisterState(new StateMachine.Core.SettingState(StateMachine,SettingCnvsGrp));
        StateMachine.SwitchState<InitState>();
    }
    public void LogApplicationInfo()
    {
        Debug.Log($"DataPath: {Application.dataPath}");
        Debug.Log($"PersistentDataPath:{Application.persistentDataPath}");
    }
}
