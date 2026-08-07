using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace StateMachine.Core
{
    public class InitState : CoreState
    {
        public InitState(CoreStateMachine stateMachine) : base(stateMachine)
        {
        }
        public override void Enter()
        {
            base.Enter();
            StateMachine.SwitchState<MainMenuState>();
        }
        public override void Exit()
        {
            base.Exit();
        }
    }

}
