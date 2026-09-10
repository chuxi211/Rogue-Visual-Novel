using Running;
using StateMachine.Rogue;
using UnityEngine;
using Setting;

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
        StateMachine =new RogueStateMachine();
        InjectContextDependency(coreContext.SettingService);
        RegisterStates(coreContext);
        StateMachine.SwitchState<InitState>();
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
        StateMachine.RegisterState(new InitState(StateMachine, coreContext.AssetBundleManager,coreContext.AudioSystem));
        StateMachine.RegisterState(new IdleState(StateMachine));
    }
}
