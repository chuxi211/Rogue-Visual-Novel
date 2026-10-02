using UnityEngine;
using System.Collections.Generic;
using Running;

namespace MainMenu
{
    public class SavedInfoPanel : MonoBehaviour
    {
        [SerializeField]
        private SavedSlot Template;
        private int MaxSlotCount;
        private List<SavedSlot> Slots = new List<SavedSlot>();
        public void Init(int maxSlotCount)
        {
            MaxSlotCount = maxSlotCount;
        }
        public void Show()
        {
            
        }
        public void Hide()
        {
            
        }
        private void SetAll()
        {
            
        }

        private void CreatBox()
        {
        }
        private void ClearBox()
        {
            if (Slots == null)
            {
                Debug.Log("CGContainers is null");
                return;
            }
            foreach (var box in Slots)
            {
                Destroy(box.gameObject);
            }
            Slots.Clear();
        }
    }
}