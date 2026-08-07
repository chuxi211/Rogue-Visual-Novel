using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCommand
{
    public readonly IClick Click;
    public InputCommand(IClick click)
    {
        if(click == null)
        {
            Debug.LogError("InputCommand: nodeClick is null");
            return;
        }
        if(click!=null)
        Click = click;
    }
}