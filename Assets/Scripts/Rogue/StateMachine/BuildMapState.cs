using Running;
using System;
using UnityEngine;

namespace StateMachine.Rogue
{
    public class BuildMapState : RogueState
    {
        private Config.Map ConfigMap;
        public BuildMapState(RogueStateMachine stateMachine, Config.Map map) : base(stateMachine)
        {
            ConfigMap = map;
        }
        public override void Enter()
        {
            base.Enter();
            if (ConfigMap == null || ConfigMap.Nodes == null || ConfigMap.Edges == null ||
                StateMachine.Context.MapBuilder == null ||
                StateMachine.Context.RogueMapParent == null || StateMachine.Context.Camera == null)
            {
                StateMachine.SwitchState(new FailureState(StateMachine, "Map build dependencies are missing."));
                return;
            }
            bool built;
            try
            {
                ClearMap();
                built = BuildMap();
                if (built) Reset();
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
                StateMachine.SwitchState(new FailureState(StateMachine, "Map cleanup or reset failed."));
                return;
            }
            if (!built)
            {
                StateMachine.SwitchState(new FailureState(StateMachine, "Map could not be built."));
                return;
            }
            StateMachine.SwitchState<IdleState>();
        }
        public override void Exit()
        {
            base.Exit();
        }
        //每一次构建地图都会生成新的MapProgression
        private bool BuildMap()
        {
            try
            {
                Vector2 nodespacing = StateMachine.Context.SettingService.Config.DeveloperSetting.NodeSpaceing;
                Map map = StateMachine.Context.MapBuilder.BuildMap(
                    ConfigMap, StateMachine.Context.RogueMapParent,
                    nodespacing);
                if (map == null || map.Nodes.Count == 0)
                {
                    return false;
                }
                MapProgression mapProgression = new MapProgression(map);
                StateMachine.Context.BindMapProgression(mapProgression);
                return true;
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
                return false;
            }
        }
        private void Reset()
        {
            //进入新地图时，重置部分状态
            StateMachine.Context.Camera.ResetPosition();
        }
        private void ClearMap()
        {
            if (StateMachine.Context.MapProgression == null)
            {
                Debug.Log("MapProgression is null.");
                return;
            }
            if (StateMachine.Context.MapProgression.Map == null)
            {
                Debug.Log("Current Map is null.");
                return;
            }
            StateMachine.Context.MapBuilder.ClearMap(StateMachine.Context.RogueMapParent);
        }
    }
}
