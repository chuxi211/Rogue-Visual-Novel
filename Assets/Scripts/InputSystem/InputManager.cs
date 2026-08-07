using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    public InputStateMachine InputStateMachine { get; private set; } = new InputStateMachine();
    public void Awake()
    {
        if(Instance!=null&&Instance!= this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(Instance);
        InputStateMachine.SwitchState(new IdleInputState());
    }
    private void Update()
    {
        InputStateMachine.Tick();
    }
}
