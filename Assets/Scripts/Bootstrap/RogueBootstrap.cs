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
    public void InitStateMachine(CoreContext coreContext)
    {
        if (PlayerParent == null)
        {
            Debug.LogError("RogueController initialization failed: PlayerParent is null.");
            return;
        }
        StateMachine = new RogueStateMachine();
        InjectContextDependency(coreContext.SettingService);
        RegisterStates(coreContext);
        StateMachine.SwitchState<StateMachine.Rogue.InitState>();
    }
    private void InjectContextDependency(SettingService settingService)
    {
        StateMachine.BindPlayerParent(PlayerParent);
        StateMachine.BindMapParent(RogueMapParent);
        StateMachine.BindSettingService(settingService);
        StateMachine.BindRogueCamera(RogueCamera);
    }
    private void RegisterStates(CoreContext coreContext)
    {
        StateMachine.RegisterState(new StateMachine.Rogue.InitState(
            StateMachine,
            coreContext.AssetBundleManager,
            coreContext.AudioSystem,
            coreContext.InputManager));
        StateMachine.RegisterState(new IdleState(StateMachine));
    }
}
