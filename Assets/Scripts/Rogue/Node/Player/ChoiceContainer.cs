
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceContainer : MonoBehaviour
{
    public TextMeshProUGUI Choice;
    public Image BG;
    private void Awake()
    {
        if(Choice == null)
        {
            Choice = GetComponentInChildren<TextMeshProUGUI>();
        }
        if(BG == null)
        {
            BG = GetComponentInChildren<Image>();
        }
    }
}
