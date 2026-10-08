using Input;
using MainMenu;
using MainMenu.Input;
using Setting;
using Setting.Config;
using StateMachine.Core;
using System;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class CoreBootstrap : MonoBehaviour
{
    public static CoreBootstrap Instance { get; private set; }
    public CoreStateMachine StateMachine { get; private set; }
    public DefaultSettingConfig DefaultSettingConfig;
    public CanvasGroup Menu;
    public CanvasGroup GalleryCnvsGrp;
    public CanvasGroup SettingCnvsGrp;
    public CanvasGroup SavedInfoCnvsGrp;
    public CGPlayer CGPlayer;
    public SavedInfoPanel SavedPanel;
    public Transform AudioPlayers;
    public List<MainMenuInput> MainMenuInputs;
    public List<SettingInput> SettingInputs;
    public List<SavedPanelInput> SavedPanelInputs;
    
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
            Instance.RebindMainMenuScene(this);
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(Instance);
        if (AudioPlayers != null) DontDestroyOnLoad(AudioPlayers);
        if (SettingArea != null) DontDestroyOnLoad(SettingArea);
        if (SavedPanel != null) DontDestroyOnLoad(SavedPanel.transform.root.gameObject);
        InitCoreStateMachine();
        LogApplicationInfo();
    }
    private void RebindMainMenuScene(CoreBootstrap sceneBootstrap)
    {
        if (StateMachine?.Context == null)
        {
            Debug.LogError("Cannot rebind MainMenu: CoreStateMachine is unavailable.");
            return;
        }

        if (StateMachine.Context.InputManager == null || StateMachine.Context.SettingService?.Config?.DeveloperSetting == null)
        {
            Debug.LogError("Cannot rebind MainMenu: input or settings are unavailable.");
            return;
        }

        Menu = sceneBootstrap.Menu;
        GalleryCnvsGrp = sceneBootstrap.GalleryCnvsGrp;
        CGPlayer = sceneBootstrap.CGPlayer;
        MainMenuInputs = sceneBootstrap.MainMenuInputs;
        StateMachine.RegisterState(new StateMachine.Core.MainMenuState(StateMachine, Menu));
        if (sceneBootstrap.CGPlayer != null)
        {
            sceneBootstrap.CGPlayer.InitCGLoader(new CGLoader(StateMachine.Context.AssetBundleManager));
            StateMachine.RegisterState(new GalleryModeState(
                StateMachine, sceneBootstrap.GalleryCnvsGrp, sceneBootstrap.CGPlayer));
        }
        if (sceneBootstrap.MainMenuInputs != null)
        {
            foreach (MainMenuInput input in sceneBootstrap.MainMenuInputs)
            {
                if (input == null) continue;
                input.BindInput(StateMachine.Context.InputManager);
                input.InitSE(
                    StateMachine.Context.SettingService.Config.DeveloperSetting.ButtonSE,
                    StateMachine.Context.AudioSystem);
            }
        }

        if (sceneBootstrap.SavedPanel != null)
        {
            sceneBootstrap.SavedPanel.transform.root.gameObject.SetActive(false);
            Destroy(sceneBootstrap.SavedPanel.transform.root.gameObject);
        }
        if (sceneBootstrap.SettingArea != null)
        {
            sceneBootstrap.SettingArea.root.gameObject.SetActive(false);
            Destroy(sceneBootstrap.SettingArea.root.gameObject);
        }
        if (sceneBootstrap.AudioPlayers != null)
            Destroy(sceneBootstrap.AudioPlayers.root.gameObject);
        if (sceneBootstrap.InputCollector != null)
            Destroy(sceneBootstrap.InputCollector.gameObject);
        if (sceneBootstrap.InputManager != null)
            Destroy(sceneBootstrap.InputManager.gameObject);
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
            SavedPanel,
            CGPlayer,
            MainMenuInputs,
            SettingInputs,
            SavedPanelInputs,
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
        StateMachine.RegisterState(new GalleryModeState(StateMachine, GalleryCnvsGrp, CGPlayer));
        StateMachine.RegisterState(new StateMachine.Core.SavedModeState(StateMachine, SavedInfoCnvsGrp,SavedPanel));
        StateMachine.RegisterState(new StateMachine.Core.SettingState(StateMachine,SettingCnvsGrp));
        StateMachine.SwitchState<InitState>();
    }
    public void LogApplicationInfo()
    {
        Debug.Log($"DataPath: {Application.dataPath}");
        Debug.Log($"PersistentDataPath:{Application.persistentDataPath}");
    }
}
