using Input;
using UnityEngine;

namespace MainMenu.Input
{
    public abstract class SavedPanelInput : MonoBehaviour, IClick
    {
        protected InputManager InputManager;
        public void BindInput(InputManager inputManager)
        {
            if (inputManager == null)
            {
                Debug.LogError(
                    $"{GetType().Name} failed to bind input: " +
                    "InputManager is null.");
                return;
            }
            InputManager = inputManager;
            return;
        }
        public abstract void Click();

        public abstract void Execute();
    }
}