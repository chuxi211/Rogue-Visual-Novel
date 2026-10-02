using Audio;
using Input;
using UnityEngine;

namespace MainMenu.Input
{
    public abstract class SettingInput : MonoBehaviour, IInput
    {
        protected InputManager InputManager;

        public bool BindInput(InputManager inputManager)
        {
            if (inputManager == null)
            {
                Debug.LogError(
                    $"{GetType().Name} failed to bind input: " +
                    "InputManager is null.");
                return false;
            }

            InputManager = inputManager;
            return true;
        }

        protected bool TryPush()
        {
            if (InputManager == null)
            {
                Debug.LogError(
                    $"{GetType().Name} failed to push input: " +
                    "InputManager is null.");
                return false;
            }

            InputManager.Push(this);
            return true;
        }

        public abstract void Execute();
    }
}
