using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Running
{
    public class CharacterContainer : MonoBehaviour
    {
        public Image Illustration;
        public RectTransform RectTransform;
        public void Awake()
        {
            if (Illustration == null)
            {
                Illustration = GetComponentInChildren<Image>();
            }
            if (RectTransform == null)
            {
                RectTransform = GetComponentInChildren<RectTransform>();
            }
        }
        public void SetSprite(Sprite sprite)
        {
            Illustration.sprite = sprite;
        }
        public void SetPosition(Vector2 normalizedPosition)
        {
            if (RectTransform == null)
            {
                Debug.LogError("SetNormalizedPosition failed: CharacterRectTransform is null.");
                return;
            }
            RectTransform.anchorMin = normalizedPosition;
            RectTransform.anchorMax = normalizedPosition;
            RectTransform.anchoredPosition = Vector2.zero;
        }
    }
}