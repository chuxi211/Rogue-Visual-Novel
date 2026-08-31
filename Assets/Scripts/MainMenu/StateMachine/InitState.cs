using Running;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace StateMachine.Core
{
    public class InitState : CoreState
    {
        private CoreInitilaizer CoreInitilaizer;
        public InitState(CoreStateMachine stateMachine,CoreInitilaizer coreInitilaizer) : base(stateMachine)
        {
            CoreInitilaizer = coreInitilaizer;
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
            CoreInitilaizer.InitContext(StateMachine.Context);
            return false;
        }
    }

}
