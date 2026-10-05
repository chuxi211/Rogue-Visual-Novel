using UnityEngine;
namespace StateMachine.Rogue
{
    public abstract class RogueState : IState
    {
        public RogueStateMachine StateMachine { get; private set; }
        public RogueState(RogueStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }
        public virtual void Enter()
        {
            Debug.Log($"CurrentState:{this.GetType()}");
        }

        public virtual void Exit()
        {
            Debug.Log($"ExitState:{this.GetType()}");
        }

        public virtual void Suspend()
        {
            throw new System.NotImplementedException();
        }

        public virtual void Resume()
        {
            throw new System.NotImplementedException();
        }
    }
}