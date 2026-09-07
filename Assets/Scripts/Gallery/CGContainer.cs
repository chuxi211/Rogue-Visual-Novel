using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class CGContainer : MonoBehaviour
    {
        public Image CG;
        public void Set(Sprite sprite)
        {
            CG.sprite = sprite;
        }
        public void Clear()
        {
            CG.sprite = null;
        }
    }
}