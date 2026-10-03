using UnityEngine;
using System.Collections.Generic;
using Running;
using Input;

namespace MainMenu
{
    public class SavedInfoPanel : MonoBehaviour
    {
        [SerializeField]
        private SavedSlot Template;
        private int MaxSlotCount;
        private List<SavedSlot> Slots = new List<SavedSlot>();
        private InputManager InputManager;
        private SavedInfoDeserializer SavedInfoDeserializer;
        private ModeOfEnterSaveMode EnterMode;
        public void Init(int maxSlotCount,InputManager inputManager,SavedInfoDeserializer deserializer)
        {
            MaxSlotCount = maxSlotCount;
            InputManager = inputManager;
            SavedInfoDeserializer = deserializer;
        }
        public void SetEnterModeAndShow(ModeOfEnterSaveMode saveMode)
        {
            SetEnterMode(saveMode);
            Show();
        }
        public void SetEnterMode(ModeOfEnterSaveMode saveMode)
        {
            EnterMode = saveMode;
        }
        public void Show()
        {
            CreateSlot();
            for (int i = 0; i < MaxSlotCount; i++)
            {
                SavedErrorCode errorCode = SavedInfoDeserializer.TryDeserialise(i, out SavedInfo savedInfo);
                switch (errorCode)
                {
                    case SavedErrorCode.Success:
                        Slots[i].gameObject.SetActive(true);
                        Slots[i].SetEnterMode(EnterMode);
                        Slots[i].SetInfo(savedInfo);
                        break;
                    case SavedErrorCode.FileNotFound:
                        Debug.Log($"SavedInfoPanel: No saved info found for slot {i}");
                    break;
                }
            }
        }
        private void CreateSlot()
        {
            if (Slots.Count < MaxSlotCount)
            {
                for (int i = Slots.Count; i < MaxSlotCount; i++)
                {
                    SavedSlot slot = Instantiate(Template, Template.transform.parent);
                    slot.Init(InputManager,i);
                    Slots.Add(slot);
                }
            }
        }
        public void Hide()
        {
            foreach(var slot in Slots)
            {
                slot.gameObject.SetActive(false);
            }
        }
    }
}