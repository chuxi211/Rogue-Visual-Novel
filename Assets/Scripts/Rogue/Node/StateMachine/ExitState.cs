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
            StateMachine.Complete();
            EventBus.Publish(new ENodeCompleted());
        }
    }
}