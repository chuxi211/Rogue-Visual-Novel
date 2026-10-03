using Input;
using UnityEngine;

namespace MainMenu.Input
{
    public abstract class SavedPanelInput : MonoBehaviour, IClick
    {
        public abstract void Click();

        public abstract void Execute();
    }
}