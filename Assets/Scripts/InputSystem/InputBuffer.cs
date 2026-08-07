using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBuffer
{
    private Queue<InputCommand> Inputs = new Queue<InputCommand>();
    public void Push(InputCommand command)
    {
        if(command == null)
        {
            Debug.LogError("InputBuffer: command is null");
            return;
        }
        Inputs.Enqueue(command);
    }
    public bool TryConsume(out InputCommand command)
    {
        while(Inputs.Count > 0)
        {
            var input = Inputs.Dequeue();
            command = input;
            return true;
        }
        command = null;
        return false;
    }
    public void Clear()
    {
        Inputs.Clear();
    }
}