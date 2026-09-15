using Running;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Input
{

    public class NextDialogueClick : MonoBehaviour, IStepClick, IPointerClickHandler
    {
        private InputManager InputManager;
        public void BindInput(InputManager inputManager)
        {
            if (inputManager == null)
            {
                Debug.LogError(
                    "NextDialogueClick failed to bind: InputManager is null.");
                return;
            }

            InputManager = inputManager;
        }
        public void Click()
        {
            EventBus.Publish(new ERequestNextStep());
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (InputManager == null)
            {
                Debug.LogError(
                    "NextDialogueClick failed: InputManager is null.");
                return;
            }
            InputManager.InputStateMachine.Push(new InputCommand(this));
        }
    }
}
