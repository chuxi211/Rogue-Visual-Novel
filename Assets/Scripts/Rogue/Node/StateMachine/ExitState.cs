using Config;
using Running;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine.Node
{
    public class ExitState : NodeState
    {
        public ExitState(NodeStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            ClearAllVisual();
            EventBus.Publish(new ENodeCompleted());
        }
        private void ClearAllVisual()
        {
            while(StateMachine.Node.EnteredComponents.Count > 0)
            {
                IComponent cmp=StateMachine.Node.EnteredComponents.Dequeue();
                cmp.ExitOnNodeEnd();
            }
        }
    }
}