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
    public void InitStateMachine(CoreContext coreContext,EnterRogueMode mode=EnterRogueMode.New,SavedInfo savedInfo=null)
    {
        if (PlayerParent == null)
        {
            Debug.LogError("RogueController initialization failed: PlayerParent is null.");
            return;
        }
        StateMachine = new RogueStateMachine();
        InjectContextDependency(coreContext.SettingService);
        RegisterStates(coreContext,mode,savedInfo);
        StateMachine.SwitchState<StateMachine.Rogue.InitState>();
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
