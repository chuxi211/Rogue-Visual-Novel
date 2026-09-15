using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Input
{
    public class IdleInputState : IInputState
    {
        public bool CanBuffer(InputCommand command)
        {
            return false;
        }

        public bool CanHandle(InputCommand command)
        {
            return false;
        }

        public void Handle(InputCommand command)
        {
            Debug.Log("DefaultInputState: Handle command");
        }
    }
}