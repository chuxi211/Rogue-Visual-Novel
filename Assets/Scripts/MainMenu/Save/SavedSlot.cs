using Running;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace MainMenu
{
    public class SavedSlot : MonoBehaviour
    {
        public Image BG;
        public TextMeshPro Name;
        private SavedInfo Info;
        private bool IsEmpty
        {
            get
            {
                return Info == null;
            }
        }
    }
}