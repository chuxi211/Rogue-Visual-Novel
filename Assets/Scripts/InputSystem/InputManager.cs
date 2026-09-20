using UnityEngine;
namespace Input
{
    public class InputManager : MonoBehaviour
    {
        public InputStateMachine InputStateMachine { get; private set; } = new InputStateMachine();
        public void Awake()
        {
            DontDestroyOnLoad(this);
        }
        public void Init()
        {
            InputStateMachine = new InputStateMachine();
            InputStateMachine.SwitchState(new IdleInputState());
        }
        public void Push(IInput input)
        {
            if (input == null)
            {
                Debug.LogError("InputManager failed to push input: input is null.");
                return;
            }

            InputStateMachine.Push(new InputCommand(input));
        }
        public void Push(IInputState newState)
        {
            InputStateMachine.Push(newState);
        }
        public void Pop()
        {
            InputStateMachine.Pop();
        }
        public void Switch(IInputState inputState)
        {
            InputStateMachine.SwitchState(inputState);
        }
        private void Update()
        {
            InputStateMachine.Tick();
        }
    }
}
