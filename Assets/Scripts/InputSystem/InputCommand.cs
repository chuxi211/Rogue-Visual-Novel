using UnityEngine;
namespace Input
{
    public class InputCommand
    {
        public readonly IInput Input;
        public InputCommand(IInput input)
        {
            if (input == null)
            {
                Debug.LogError("InputCommand: nodeClick is null");
                return;
            }
            if (input != null)
                Input = input;
        }
    }
}