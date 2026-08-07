using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine.Node
{
    /// <summary>
    /// 暂时用作占位符
    /// </summary>
    public class IdleState : NodeState
    {
        public IdleState(NodeStateMachine stateMachine) : base(stateMachine)
        {
        }
        public override void Enter()
        {
            base.Enter();
        }
        public override void Exit()
        {
            base.Exit();
        }
    }
}