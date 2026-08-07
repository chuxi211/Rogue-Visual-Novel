using StateMachine.Rogue;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RogueController : MonoBehaviour
{
    [SerializeField]
    private Transform PlayerParent;
    public RogueStateMachine StateMachine { get; private set; }
    public void Awake()
    {
        InitStateMachine();
    }
    private void InitStateMachine()
    {
        if (PlayerParent == null)
        {
            Debug.LogError("RogueController initialization failed: PlayerParent is null.");
            return;
        }
        StateMachine =new RogueStateMachine();
        StateMachine.BindPlayerParent(PlayerParent);
        StateMachine.RegisterState(new InitState(StateMachine));
        StateMachine.RegisterState(new IdleState(StateMachine));
        StateMachine.SwitchState<InitState>();
    }
}
