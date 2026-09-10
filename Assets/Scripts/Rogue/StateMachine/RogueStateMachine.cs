using Setting;
using UnityEngine;
using Utility;
namespace StateMachine.Rogue
{
    public class RogueStateMachine : StateMachine<RogueState>
    {
        public RogueContext Context { get; private set; }
        public RogueStateMachine()
        {
            Context = new RogueContext();
        }
        public void BindPlayerParent(Transform parent)
        {
            Context.BindPlayerParent(parent);
        }
        public void BindMapParent(Transform parent)
        {
            Context.BindRogueMapParent(parent);
        }
        public void BindSettingService(SettingService settingService)
        {
            Context.BindSettingService(settingService);
        }
        public void BindRogueCamera(RogueCamera camera)
        {
            Context.BindRogueCamera(camera);
        }
    }
}
