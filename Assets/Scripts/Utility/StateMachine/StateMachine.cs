using System.Collections.Generic;
namespace Utility
{
    public class StateMachine<T> where T : IState
    {
        protected T CurrentState;
        private Dictionary<System.Type, T> States;
        private Stack<T> StateStack;
        public StateMachine()
        {
            States = new Dictionary<System.Type, T>();
            StateStack = new Stack<T>();
        }
        public void RegisterState(T state)
        {
            States[state.GetType()] = state;
        }
        public bool TryGetState<TState>(out TState state) where TState : T
        {
            if (States.TryGetValue(typeof(TState), out T foundState))
            {
                state = (TState)foundState;
                return true;
            }
            else
            {
                state = default;
                return false;
            }
        }
        public virtual void SwitchState<TState>() where TState : T
        {
            CurrentState?.Exit();
            CurrentState = States[typeof(TState)];
            CurrentState?.Enter();
        }
        public virtual void SwitchState(T newstate)
        {
            CurrentState?.Exit();
            CurrentState = newstate;
            CurrentState?.Enter();
        }
        public virtual void PushState(T newState)
        {
            if (CurrentState != null)
            {
                StateStack.Push(CurrentState);
                CurrentState.Exit();
            }
            CurrentState = newState;
            CurrentState?.Enter();
        }
        public virtual void PopState()
        {
            if (CurrentState != null)
            {
                CurrentState.Exit();
            }
            CurrentState = StateStack.Pop();
            CurrentState?.Enter();
        }
    }
}