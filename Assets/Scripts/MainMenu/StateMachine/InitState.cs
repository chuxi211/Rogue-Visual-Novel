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

            if (!Init())
            {
                Debug.LogError("Core InitState initialization failed.");
                return;
            }

            StateMachine.SwitchState<MainMenuState>();
        }
        public override void Exit()
        {
            base.Exit();
        }
        private bool Init()
        {
            if (StateMachine.Context == null)
            {
                Debug.LogError("CoreContext is null.");
                return false;
            }

            return StateMachine.Context.Init();
        }
    }

}
