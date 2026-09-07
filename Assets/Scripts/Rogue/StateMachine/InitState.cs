using Config;
using Running;
using StateMachine.Node;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Utility;

namespace StateMachine.Rogue
{
    public class InitState : RogueState
    {
        //后续所有的Init逻辑可以移至RogueInitiallizer
        private RogueInitiallizer RogueInitiallizer;
        private AssetBundleManager AssetBundleManager;
        public InitState(RogueStateMachine stateMachine,AssetBundleManager assetBundleManager) : base(stateMachine)
        {
            AssetBundleManager = assetBundleManager;
        }
        public override void Enter()
        {
            base.Enter();
            Init();
            NewGame();
        }
        public override void Exit()
        {
            base.Exit();
        }
        private void Init()
        {
            RogueInitiallizer = new RogueInitiallizer(AssetBundleManager);
            RogueInitiallizer.InitContext(StateMachine.Context);
        }

        private void NewGame()
        {
            StateMachine.Context.LayerProgression.SetLayer(1);
            if(StateMachine.Context.ConfigRepository.TryGetMap(
                StateMachine.Context.LayerProgression.CurrentLayer, out Config.Map map))
            {
                StateMachine.SwitchState(new BuildMapState(StateMachine, map));
            }
        }
        private void Load()
        {

        }
    }
}
