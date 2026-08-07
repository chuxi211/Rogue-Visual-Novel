using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputState
{
    bool CanHandle(InputCommand command);
    bool CanBuffer(InputCommand command);
    void Handle(InputCommand command);
}