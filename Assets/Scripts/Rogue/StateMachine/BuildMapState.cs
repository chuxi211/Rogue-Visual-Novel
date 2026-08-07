using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine.Rogue
{
    public class BuildMapState : RogueState
    {
        private Config.Map ConfigMap;
        public BuildMapState(RogueStateMachine stateMachine,Config.Map map) : base(stateMachine)
        {
            ConfigMap = map;
        }
        public override void Enter()
        {
            base.Enter();
            BuildMap();
            StateMachine.SwitchState<IdleState>();
        }
        public override void Exit()
        {
            base.Exit();
        }
        private void BuildMap()
        {
            StateMachine.Context.MapBuilder.BuildMap(ConfigMap);
        }
    }
}