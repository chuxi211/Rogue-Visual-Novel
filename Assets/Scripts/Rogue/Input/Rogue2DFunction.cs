using Input;
using UnityEngine;

namespace Rogue.Input
{
    public class Rogue2DFunction : MonoBehaviour, IRogueInput, IClick
    {
        protected InputManager InputManager;
        public void BindInputManager(InputManager inputManager)
        {
            InputManager = inputManager;
        }
        public virtual void Click()
        {
            throw new System.NotImplementedException();
        }

        public virtual void Execute()
        {
            Click();
        }
    }
}