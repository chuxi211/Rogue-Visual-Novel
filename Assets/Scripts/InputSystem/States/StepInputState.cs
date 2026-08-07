using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepInputState : IInputState
{
    public bool CanBuffer(InputCommand command)
    {
        return false;
    }
    public bool CanHandle(InputCommand command)
    {
        return command != null && command.Click is IStepClick;
    }
    public void Handle(InputCommand command)
    {
        command.Click.Click();
    }
}