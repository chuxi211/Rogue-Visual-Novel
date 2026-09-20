using Input;
using System.Collections;
using TMPro;
using UnityEngine;

public class DialoguePlayer : MonoBehaviour
{
    public Transform DialogueContainer;
    public TextMeshProUGUI TextMeshPro;
    private Coroutine TypingCoro;
    public InputManager InputManager;
    private void Awake()
    {
        if (DialogueContainer == null)
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
    public void SetText(string text, bool typewriterEnable = true,int charPerSec=30)
    {
        if(TextMeshPro == null)
        {
            TextMeshPro = DialogueContainer.GetComponentInChildren<TextMeshProUGUI>();
            if (TextMeshPro == null)
            {
                return;
            }
        }
        if (TypingCoro != null)
        {
            StopCoroutine(TypingCoro);
        }
        if (typewriterEnable)
        {
            TypingCoro = StartCoroutine(Typing(TextMeshPro, text, charPerSec));
        }
        else
        {
            TextMeshPro.text = text;
        }
    }
    public void ClearText()
    {
        if (TypingCoro != null)
        {
            StopCoroutine(TypingCoro);
        }
        TextMeshPro.text= string.Empty;
    }
    public void Hide()
    {
        if (TypingCoro != null)
        {
            StopCoroutine(TypingCoro);
        }
        DialogueContainer.gameObject.SetActive(false);
    }
    public void Show()
    {

        DialogueContainer.gameObject.SetActive(true);
    }
    private IEnumerator Typing(TextMeshProUGUI textMesh, string text,int charPerSec)
    {
        for (int i = 0; i < text.Length; i++)
        {
            textMesh.text += text[i];
            yield return new WaitForSeconds(1f/charPerSec);
        }
        TypingCoro = null;
    }
}
