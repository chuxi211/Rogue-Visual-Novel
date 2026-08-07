using Running;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NextDialogueClick : MonoBehaviour,IStepClick,IPointerClickHandler
{
    public void Click()
    {
        EventBus.Publish(new ERequestNextStep());
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (InputManager.Instance == null)
        {
            Debug.LogError(
                "ContinueDialogueClick failed: InputManager.Instance is null.");
            return;
        }
        InputManager.Instance.InputStateMachine.Push(new InputCommand(this));
    }
}
