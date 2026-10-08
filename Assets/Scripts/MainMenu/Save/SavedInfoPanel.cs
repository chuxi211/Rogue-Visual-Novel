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
        public List<SavedSlot> Slots { get; private set; } = new List<SavedSlot>();
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
                Slots[i].gameObject.SetActive(false);
                SavedErrorCode errorCode = SavedInfoDeserializer.TryDeserialise(i, out SavedInfo savedInfo);
                switch (errorCode)
                {
                    case SavedErrorCode.Success:
                    case SavedErrorCode.FileNotFound:
                        Slots[i].SetEnterMode(EnterMode);
                        Slots[i].SetInfo(savedInfo);
                        Slots[i].gameObject.SetActive(
                            EnterMode == ModeOfEnterSaveMode.Save || savedInfo != null);
                        break;
                    case SavedErrorCode.InvalidData:
                        Debug.LogError($"Save slot {i} contains invalid data.");
                        Slots[i].SetEnterMode(EnterMode);
                        Slots[i].SetInfo(null);
                        Slots[i].gameObject.SetActive(EnterMode == ModeOfEnterSaveMode.Save);
                        break;
                    default:
                        Debug.LogError($"Cannot read save slot {i}: {errorCode}.");
                        break;
                }
            }
        }
        public void Refresh()
        {
            Show();
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
