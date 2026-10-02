
using Audio;
using Display;
using Input;
using MainMenu.Input;
using Running;
using Setting;
using Setting.Config;
using Setting.Running;
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
        public SavedInfoSerializer SavedInfoSerializer { get; private set; }
        public SavedInfoDeserializer SavedInfoDeserializer { get; private set; }
        public CGPlayer CGPlayer { get; private set; }
        public CGLoader CGLoader { get; private set; }
        public List<MainMenuInput> MainMenuInputs { get; private set; }
        public List<SettingInput> SettingInputs { get; private set; }
        public Transform AudioPlayerParent { get; private set; }
        public InputCollector InputCollector { get; private set; }
        public InputManager InputManager { get; private set; }
        public AudioArea AudioArea { get; private set; }
        public DisplayArea DisplayArea { get; private set; }
        public InputArea InputArea { get; private set; }
        public TextArea TextArea { get; private set; }
        public CoreInitilaizer(
            SettingLoader settingLoader,
            CGPlayer cgPlayer,
            List<MainMenuInput> minputs,
            List<SettingInput> sinputs,
            Transform audioPlayerParent,
            InputCollector inputCollector,
            InputManager inputManager,
            AudioArea audioArea,
            DisplayArea displayArea,
            InputArea inputArea,
            TextArea textArea)
        {
            SettingLoader = settingLoader;
            CGPlayer = cgPlayer;
            MainMenuInputs = minputs;
            SettingInputs = sinputs;
            AudioPlayerParent = audioPlayerParent;
            InputCollector = inputCollector;
            InputManager = inputManager;
            AudioArea = audioArea;
            DisplayArea = displayArea;
            InputArea = inputArea;
            TextArea = textArea;
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
            #region Initialize Setting
            if (!InitSettingService(out SettingService settingService))
            {
                return false;
            }
            #endregion
            if (!settingService.GetConfigSetting<DeveloperSettings>(out DeveloperSettings dvlpSetting))
            {
                return false;
            }

            string root = dvlpSetting.AssetBundlesRoot;

            if (string.IsNullOrEmpty(root))
            {
                Debug.LogError(
                    "CoreContext initialization failed: " +
                    "AssetBundlesRoot is null or empty.");
                return false;
            }
            #region Initialize AssetBundleManager
            if (!InitAssetBundleManager(root, out AssetBundleManager assetBundleManager))
            {
                return false;
            }
            #endregion
            #region Initialize CGPlayer
            if (!InitCGPlayer())
            {
                return false;
            }
            #endregion
            string bundlename = dvlpSetting.AudioDefinitionCatalogBundle;
            string assetname = dvlpSetting.AudioDefinitionCatalogAsset;
            #region Initialize Audio
            if (!InitAudioDefinitionRepository(bundlename, assetname, out AudioDefinitionRepository audioDefinitionRepository))
            {
                return false;
            }
            if (!InitAudioSystem(
                audioDefinitionRepository,
                settingService.GetAudioSetting(),
                out AudioSystem audioSystem))
            {
                return false;
            }
            #endregion
            #region Initialize Display
            if (!InitDisplaySystem(settingService.GetDisplaySetting(), out DisplaySystem displaySystem))
            {
                return false;
            }
            #endregion
            if (!InitSavedServices(dvlpSetting.SavedInfoDire, dvlpSetting.SavedInfoName, dvlpSetting.AutoSavedInfoName))
            {
                return false;
            }
            InitInputs(InputManager, dvlpSetting.ButtonSE, audioSystem);
            if (!InitInputSystem())
            {
                return false;
            }

            if (!InitSettingAreas(InputManager, settingService))
            {
                return false;
            }

            coreContext.BindSettingService(settingService);
            coreContext.BindABManager(assetBundleManager);
            coreContext.BindAudioSystem(audioSystem);
            coreContext.BindDisplaySystem(displaySystem);
            coreContext.BindAudioDefinitionRepository(audioDefinitionRepository);
            coreContext.BindInputManager(InputManager);
            coreContext.BindAudioArea(AudioArea);
            coreContext.BindDisplayArea(DisplayArea);
            coreContext.BindInputArea(InputArea);
            coreContext.BindTextArea(TextArea);
            coreContext.BindSavedInfoService(SavedInfoSerializer, SavedInfoDeserializer);
            return true;
        }
        public bool InitAudioSystem(
            AudioDefinitionRepository audioDefinitionRepository,
            AudioSetting audioSetting,
            out AudioSystem audioSystem)
        {
            audioSystem = null;

            if (AssetBundleManager == null)
            {
                Debug.LogError(
                    "AudioSystem initialization failed: " +
                    "AssetBundleManager is null.");
                return false;
            }

            if (audioDefinitionRepository == null || !audioDefinitionRepository.IsValid)
            {
                Debug.LogError(
                    "AudioSystem initialization failed: " +
                    "AudioDefinitionRepository is null or invalid.");
                return false;
            }

            List<AudioPlayer> audioPlayers = GetAudioPlayers();

            if (audioPlayers.Count == 0)
            {
                return false;
            }

            bool hasSoundPlayer = audioPlayers.Exists(
                audioPlayer => audioPlayer is SoundPlayer);
            bool hasDialogueAudioPlayer = audioPlayers.Exists(
                audioPlayer => audioPlayer is DialogueAudioPlayer);

            if (!hasSoundPlayer)
            {
                Debug.LogError(
                    "AudioSystem initialization failed: " +
                    "SoundPlayer was not found.");
                return false;
            }

            if (!hasDialogueAudioPlayer)
            {
                Debug.LogError(
                    "AudioSystem initialization failed: " +
                    "DialogueAudioPlayer was not found.");
                return false;
            }

            audioSystem = new AudioSystem(
                audioPlayers,
                AssetBundleManager,
                audioDefinitionRepository,
                audioSetting);
            return true;
        }

        private bool InitDisplaySystem(DisplaySetting displaySetting,out DisplaySystem displaySystem)
        {
            displaySystem = null;
            if (displaySetting == null)
            {
                return false;
            }
            displaySystem=new DisplaySystem(displaySetting);
            return true;
        }
        private List<AudioPlayer> GetAudioPlayers()
        {
            List<AudioPlayer> audioPlayers = new List<AudioPlayer>();
            if (AudioPlayerParent == null)
            {
                Debug.LogError(
                    "CoreContext initialization failed: " +
                    "AudioPlayerParent is null.");
                return audioPlayers;
            }
            AudioPlayer[] players = AudioPlayerParent.GetComponentsInChildren<AudioPlayer>(true);
            if (players == null || players.Length == 0)
            {
                Debug.LogError(
                    "CoreContext initialization failed: " +
                    "No AudioPlayers found in AudioPlayerParent.");
                return audioPlayers;
            }
            audioPlayers.AddRange(players);
            return audioPlayers;
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

        public bool InitSettingService(out SettingService settingService)
        {
            settingService = null;
            if (SettingLoader == null)
            {
                Debug.LogError(
                    "CoreContext initialization failed: " +
                    "SettingLoader is null.");
                return false;
            }

            SettingInitializer = new SettingInitializer(SettingLoader);

            if (!SettingInitializer.TryInitSetting(out settingService))
            {
                Debug.LogError(
                    "CoreContext initialization failed: " +
                    "SettingService initialization failed.");
                return false;
            }

            return true;
        }
        public bool InitCGPlayer()
        {
            if (CGPlayer == null)
            {
                Debug.LogError("CoreContext initialization failed: CGPlayer is null.");
                return false;
            }
            CGLoader = new CGLoader(AssetBundleManager);
            CGPlayer.InitCGLoader(CGLoader);
            return true;
        }
        public bool InitAudioDefinitionRepository(string bundlename, string assetname, out AudioDefinitionRepository audioDefinitionRepository)
        {
            audioDefinitionRepository = null;
            //实例AssetDefinitionLoader
            AudioDefinitionsLoader audioDefinitionsLoader = new AudioDefinitionsLoader(AssetBundleManager);
            //加载所有Definition
            if (!audioDefinitionsLoader.TryLoad(bundlename, assetname, out AudioDefinitionCatalog definitions))
            {
                Debug.LogError("CoreContext initialization failed: AudioDefinitionRepository initialization failed.");
                audioDefinitionRepository = null;
                return false;
            }

            if (definitions == null || definitions.AudioDefinitions == null)
            {
                Debug.LogError(
                    "CoreContext initialization failed: " +
                    "AudioDefinitionCatalog or its AudioDefinitions list is null.");
                return false;
            }

            //将Definition存入AudioDefinitionRepository
            audioDefinitionRepository = new AudioDefinitionRepository(definitions.AudioDefinitions.ToArray());

            if (!audioDefinitionRepository.IsValid)
            {
                Debug.LogError(
                    "CoreContext initialization failed: " +
                    "AudioDefinitionRepository contains invalid definitions.");
                audioDefinitionRepository = null;
                return false;
            }

            return true;
        }
        public void InitInputs(InputManager inputManager, AudioID audioID, ISoundPlayback soundPlayback)
        {
            if (MainMenuInputs == null || MainMenuInputs.Count == 0
                ||SettingInputs==null||SettingInputs.Count==0)
            {
                Debug.LogWarning("CoreContext initialization warning: Buttons list is null or empty.");
                return;
            }
            if (audioID == null)
            {
                Debug.LogWarning("CoreContext initialization warning: AudioID for button sound effects is null.");
                return;
            }
            foreach (var input in MainMenuInputs)
            {
                if (input == null)
                {
                    Debug.LogWarning("CoreContext initialization warning: A button in the list is null.");
                    continue;
                }
                input.InitSE(audioID, soundPlayback);
                input.BindInput(inputManager);
            }
            foreach(var input in SettingInputs)
            {
                if(input == null)
                {
                    Debug.LogWarning("CoreContext initialization warning: A button in the list is null.");
                    continue;
                }
                input.BindInput(inputManager);
            }

        }
        public bool InitInputSystem()
        {
            if (InputManager == null)
            {
                Debug.LogError(
                    "Input initialization failed: InputManager is null.");
                return false;
            }

            if (InputCollector == null)
            {
                Debug.LogError(
                    "Input initialization failed: InputCollector is null.");
                return false;
            }

            InputManager.Init();
            InputCollector.Bind(InputManager);
            return true;
        }
        public bool InitSettingAreas(
            InputManager inputManager,
            SettingService settingService)
        {
            if (inputManager == null)
            {
                Debug.LogError(
                    "Setting areas initialization failed: " +
                    "InputManager is null.");
                return false;
            }

            if (settingService == null)
            {
                Debug.LogError(
                    "Setting areas initialization failed: " +
                    "SettingService is null.");
                return false;
            }

            if (AudioArea == null || DisplayArea == null ||
                InputArea == null || TextArea == null)
            {
                Debug.LogError(
                    "Setting areas initialization failed: " +
                    "one or more setting areas are null.");
                return false;
            }

            bool success = true;
            success &= AudioArea.Init(
                inputManager,
                settingService,
                settingService);
            success &= DisplayArea.Init(
                inputManager,
                settingService,
                settingService);
            success &= InputArea.Init(
                inputManager,
                settingService,
                settingService);
            success &= TextArea.Init(
                inputManager,
                settingService,
                settingService);
            return success;
        }
        public bool InitSavedServices(string direName,string savedInfoName,string autoSavedInfoName)
        {
            SavedInfoSerializer = new SavedInfoSerializer(direName,savedInfoName,autoSavedInfoName);
            SavedInfoDeserializer = new SavedInfoDeserializer(direName,savedInfoName,autoSavedInfoName);
            return true;
        }
    }
}
