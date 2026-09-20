using UnityEngine;
namespace StateMachine.Node
{
    public abstract class NodeState : IState
    {
        public NodeStateMachine StateMachine { get; private set; }
        public NodeState(NodeStateMachine stateMachine)
        {
            this.StateMachine = stateMachine;
            if (StateMachine == null)
            {
                Debug.LogError("NodeStateMachine is null");
            }
        }
        public virtual void Enter()
        {
            Debug.Log($"CurrentState:{this.GetType()}");
        }

        public virtual void Exit()
        {
            Debug.Log($"ExitState:{this.GetType()}");
        }
    }
}