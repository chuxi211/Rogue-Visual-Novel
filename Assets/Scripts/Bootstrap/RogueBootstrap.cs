using Running;
using Setting;
using StateMachine.Core;
using StateMachine.Rogue;
using UnityEngine;

public class RogueBootstrap : MonoBehaviour
{
    [SerializeField]
    private Transform PlayerParent;
    [SerializeField]
    private Transform RogueMapParent;
    [SerializeField]
    private RogueCamera RogueCamera;
    public RogueStateMachine StateMachine { get; private set; }
    public SavedInfo CreateSavedInfo()
    {
        var context = StateMachine?.Context;
        var progression = context?.MapProgression;
        var node = progression?.CurrentNode;
        if (progression?.Map == null || node == null ||
            string.IsNullOrEmpty(node.ID) || context.LayerProgression == null)
        {
            return null;
        }

        return new SavedInfo
        {
            CurrentMapID = progression.Map.ID,
            CurrentNodeID = node.ID,
            IsNodeCompleted = node.IsCompleted,
            CurrentLayer = context.LayerProgression.CurrentLayer,
            Position = node.Position
        };
    }
    public bool InitStateMachine(CoreContext coreContext,EnterRogueMode mode=EnterRogueMode.New,SavedInfo savedInfo=null)
    {
        if (coreContext == null || coreContext.InputManager == null ||
            coreContext.AssetBundleManager == null || coreContext.AudioSystem == null ||
            coreContext.SettingService?.Config?.DeveloperSetting == null ||
            PlayerParent == null || RogueMapParent == null || RogueCamera == null)
        {
            Debug.LogError("RogueController initialization failed: a required dependency is null.");
            return false;
        }
        try
        {
            StateMachine = new RogueStateMachine();
            InjectContextDependency(coreContext.SettingService);
            foreach (var input in FindObjectsByType<Rogue.Input.Rogue2DFunction>(
                FindObjectsInactive.Include, FindObjectsSortMode.None))
            {
                input.BindInputManager(coreContext.InputManager);
            }
            RegisterStates(coreContext,mode,savedInfo);
            StateMachine.SwitchState<StateMachine.Rogue.InitState>();
            return true;
        }
        catch (System.Exception exception)
        {
            Debug.LogException(exception);
            return false;
        }
    }
    private void InjectContextDependency(SettingService settingService)
    {
        StateMachine.BindPlayerParent(PlayerParent);
        StateMachine.BindMapParent(RogueMapParent);
        StateMachine.BindSettingService(settingService);
        StateMachine.BindRogueCamera(RogueCamera);
    }
    private void RegisterStates(CoreContext coreContext, EnterRogueMode mode, SavedInfo savedInfo)
    {
        StateMachine.RegisterState(new StateMachine.Rogue.InitState(
            StateMachine,
            coreContext.AssetBundleManager,
            coreContext.AudioSystem,
            coreContext.InputManager,
            mode,
            savedInfo));
        StateMachine.RegisterState(new IdleState(StateMachine));
        StateMachine.RegisterState(new StateMachine.Rogue.ExitState(StateMachine));
    }
}
