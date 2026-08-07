using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.Core;

public class CoreController : MonoBehaviour
{
    public static CoreController Instance { get; private set; }
    public CoreStateMachine StateMachine { get; private set; }
    private void Awake()
    {
        if(Instance != null&& Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;
        DontDestroyOnLoad(Instance);
        InitCoreStateMachine();
    }
    private void InitCoreStateMachine()
    {
        StateMachine = new CoreStateMachine();
        StateMachine.RegisterState(new InitState(StateMachine));
        StateMachine.RegisterState(new MainMenuState(StateMachine));
        StateMachine.SwitchState<InitState>();
    }
}
