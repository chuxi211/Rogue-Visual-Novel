using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Input
{
    public class InputStateMachine
    {
        private readonly InputBuffer InputBuffer = new InputBuffer();
        public IInputState CurrentState { get; private set; }
        public void SwitchState(IInputState newstate)
        {
            if (newstate == null)
            {
                Debug.Log("InputStateMachine: newstate is null");
                return;
            }
            CurrentState = newstate;
        }
        public void Push(InputCommand input)
        {
            //能执行的直接执行，不能的进缓存队列，都不能的舍弃
            if (CurrentState.CanHandle(input))
            {
                CurrentState.Handle(input);
                return;
            }
            if (CurrentState.CanBuffer(input))
            {
                InputBuffer.Push(input);
            }
        }
        public void Tick()
        {
            if (InputBuffer.TryConsume(out var command))
            {
                if (CurrentState.CanHandle(command))
                {
                    CurrentState.Handle(command);
                }
                else if (CurrentState.CanBuffer(command))
                {
                    InputBuffer.Push(command);
                }
            }
        }
    }
}
