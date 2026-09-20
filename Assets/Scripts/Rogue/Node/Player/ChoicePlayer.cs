using Config;
using Input;
using System.Collections.Generic;
using UnityEngine;

public class ChoicePlayer : MonoBehaviour
{
    public ChoiceContainer Template;
    public List<ChoiceContainer> ChoiceContainers = new List<ChoiceContainer>();
    private InputManager InputManager;
    public void BindInput(InputManager inputManager)
    {
        if (inputManager == null)
        {
            Debug.LogError(
                "ChoicePlayer failed to bind input: InputManager is null.");
            return;
        }

        InputManager = inputManager;

        ChoiceContainers.ForEach(container =>
        {
            if (container == null)
            {
                Debug.LogWarning(
                    "ChoicePlayer could not bind input to a null ChoiceContainer.");
                return;
            }

            container.BindInput(inputManager);
        });
    }
    public void SetChoicesAndShow(List<ChoiceInfo> choices)
    {
        if (choices == null)
        {
            Debug.LogError(
                "ChoicePlayer failed to show choices: Choices is null.");
            return;
        }

        if (InputManager == null)
        {
            Debug.LogError(
                "ChoicePlayer failed to show choices: InputManager is null.");
            return;
        }

        if (Template == null)
        {
            Debug.LogError(
                "ChoicePlayer failed to show choices: Template is null.");
            return;
        }

        int ActiveCount = choices.Count;

        while (ChoiceContainers.Count < ActiveCount)
        {
            ChoiceContainer NewChoiceContainer = Instantiate(Template, Template.transform.parent);

            if (NewChoiceContainer == null)
            {
                Debug.LogError(
                    "ChoicePlayer: Failed to create ChoiceContainer.");
                return;
            }
            NewChoiceContainer.BindInput(InputManager);
            ChoiceContainers.Add(NewChoiceContainer);
        }

        for (int Index = 0; Index < ChoiceContainers.Count; Index++)
        {
            ChoiceContainer Container = ChoiceContainers[Index];

            if (Container == null)
            {
                Debug.LogError(
                    $"ChoicePlayer: ChoiceContainer at index {Index} is null.");
                continue;
            }

            bool IsActive = Index < ActiveCount;
            Container.gameObject.SetActive(IsActive);

            if (IsActive)
            {
                Container.SetChoiceText(choices[Index].Text);
                Container.BindID(choices[Index].NextID);
            }
            else
            {
                Container.SetChoiceText(string.Empty);
            }
        }
    }
    public void Hide()
    {
        ChoiceContainers.ForEach(c => c.gameObject.SetActive(false));
    }
    public void ClearAllChoicesText()
    {
        ChoiceContainers.ForEach(container => container.SetChoiceText(string.Empty));
    }
}
