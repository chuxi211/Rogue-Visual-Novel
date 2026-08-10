using Config;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoicePlayer : MonoBehaviour
{
    public ChoiceContainer Template;
    public List<ChoiceContainer> ChoiceContainers=new List<ChoiceContainer>();
    public void SetChoicesAndShow(List<ChoiceInfo> choices)
    {
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
        ChoiceContainers.ForEach(container=>container.SetChoiceText(string.Empty));
    }
}
