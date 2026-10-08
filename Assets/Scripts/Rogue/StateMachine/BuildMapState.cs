using Running;
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
            ClearMap();
            BuildMap();
            Reset();
            StateMachine.SwitchState<IdleState>();
        }
        public override void Exit()
        {
            base.Exit();
        }
        //每一次构建地图都会生成新的MapProgression
        private void BuildMap()
        {
            Vector2 nodespacing = StateMachine.Context.SettingService.Config.DeveloperSetting.NodeSpaceing;
            Map map = StateMachine.Context.MapBuilder.BuildMap(
                ConfigMap, StateMachine.Context.RogueMapParent,
                nodespacing);
            MapProgression mapProgression = new MapProgression(map);
            StateMachine.Context.BindMapProgression(mapProgression);
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
