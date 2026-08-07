using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace StateMachine.Core
{
    public abstract class CoreState : IState
    {
        public CoreStateMachine StateMachine { get; private set; }
        public CoreState(CoreStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }
        public virtual void Enter()
        {
            Debug.Log($"CurrentState:{this.GetType()}");
        }

        public virtual void Exit()
        {
            Debug.Log($"ExitState:{this.GetType()}");
        }
    }
}