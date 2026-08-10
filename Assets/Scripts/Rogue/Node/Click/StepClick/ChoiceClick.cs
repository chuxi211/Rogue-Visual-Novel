using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Running;

public class ChoiceClick : MonoBehaviour, IStepClick,IPointerClickHandler
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
        EventBus.Publish(new ERequestStepByID(ChoiceContainer.NextID));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(InputManager.Instance == null)
        {
            Debug.LogError(
                "ChoiceClick failed: InputManager.Instance is null.");
            return;
        }
        InputManager.Instance.InputStateMachine.Push(new InputCommand(this));
    }
}