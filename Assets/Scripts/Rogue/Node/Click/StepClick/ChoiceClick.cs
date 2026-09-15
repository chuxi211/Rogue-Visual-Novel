using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Running;
namespace Input
{

    public class ChoiceClick : MonoBehaviour, IStepClick, IPointerClickHandler
    {
        [SerializeField] private ChoiceContainer ChoiceContainer;
        private void Awake()
        {
            if (ChoiceContainer == null)
            {
                ChoiceContainer = GetComponentInParent<ChoiceContainer>();
            }
        }
        public void Click()
        {
            if (ChoiceContainer == null)
            {
                Debug.LogError(
                    "ChoiceClick failed: ChoiceContainer is null.");
                return;
            }

            EventBus.Publish(new ERequestStepByID(ChoiceContainer.NextID));
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (ChoiceContainer == null)
            {
                Debug.LogError(
                    "ChoiceClick failed: ChoiceContainer is null.");
                return;
            }

            if (ChoiceContainer.InputManager == null)
            {
                Debug.LogError(
                    "ChoiceClick failed: InputManager is null.");
                return;
            }
            ChoiceContainer.InputManager.InputStateMachine.Push(new InputCommand(this));
        }
    }
}
