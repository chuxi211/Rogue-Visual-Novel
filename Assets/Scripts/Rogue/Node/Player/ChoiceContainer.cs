
using Config;
using Input;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceContainer : MonoBehaviour
{
    public TextMeshProUGUI Choice;
    public Image BG;
    public StepID NextID { get; private set; }
    public InputManager InputManager { get; private set; }
    private void Awake()
    {
        if (Choice == null)
        {
            Choice = GetComponentInChildren<TextMeshProUGUI>();
        }
        if (BG == null)
        {
            BG = GetComponentInChildren<Image>();
        }
    }
    public void BindID(StepID nextID)
    {
        NextID = nextID;
    }
    public void BindInput(InputManager inputManager)
    {
        if (inputManager == null)
        {
            Debug.LogError(
                "ChoiceContainer failed to bind input: InputManager is null.");
            return;
        }

        InputManager = inputManager;
    }
    public void SetChoiceText(string text)
    {
        if (Choice == null)
        {
            Debug.LogError("ChoiceContainer: Choice Text is null");
            return;
        }
        Choice.text = text;
    }
}
