using Config;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialoguePlayer : MonoBehaviour
{
    public Transform DialogueContainer;
    private void Awake()
    {
        if(DialogueContainer == null)
        {
            DialogueContainer = GetComponentInChildren<Transform>();
        }
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
