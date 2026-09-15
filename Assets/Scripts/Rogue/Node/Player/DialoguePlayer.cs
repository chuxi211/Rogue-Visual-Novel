using Config;
using Input;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialoguePlayer : MonoBehaviour
{
    public Transform DialogueContainer;
    public InputManager InputManager;
    private void Awake()
    {
        if(DialogueContainer == null)
        {
            DialogueContainer = GetComponentInChildren<Transform>();
        }
    }
    public void BindInput(InputManager inputManager)
    {
        if (inputManager == null)
        {
            Debug.LogError(
                "DialoguePlayer failed to bind input: InputManager is null.");
            return;
        }

        if (DialogueContainer == null)
        {
            Debug.LogError(
                "DialoguePlayer failed to bind input: DialogueContainer is null.");
            return;
        }

        NextDialogueClick click =
            DialogueContainer.GetComponentInChildren<NextDialogueClick>(true);
        if (click == null)
        {
            Debug.LogError(
                "DialoguePlayer failed to bind input: " +
                "NextDialogueClick was not found.");
            return;
        }

        InputManager = inputManager;
        click.BindInput(InputManager);
    }
    public void SetText(string text)
    {
        DialogueContainer.GetComponentInChildren<TextMeshProUGUI>().text = text;
    }
    public void ClearText()
    {
        DialogueContainer.GetComponentInChildren<TextMeshProUGUI>().text = string.Empty;
    }
    public void Hide()
    {
        DialogueContainer.gameObject.SetActive(false);
    }
    public void Show()
    {

        DialogueContainer.gameObject.SetActive(true);
    }
}
