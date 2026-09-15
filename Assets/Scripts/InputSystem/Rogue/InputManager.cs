using System.Collections;
using System.Collections.Generic;
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
            InputStateMachine=new InputStateMachine();
            InputStateMachine.SwitchState(new IdleInputState());
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
