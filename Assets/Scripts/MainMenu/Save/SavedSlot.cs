using Input;
using MainMenu.Input;
using Running;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace MainMenu
{
    public class SavedSlot : MonoBehaviour
    {
        private int Index;
        public Image BG;
        public TextMeshPro Name;
        public SavedClick Click;
        private SavedInfo Info;
        public bool IsEmpty
        {
            get
            {
                return Info == null;
            }
        }
        public void SetEnterMode(ModeOfEnterSaveMode enterMode)
        {
            Click.SetEnterMode(enterMode);
        }
        public void Init(InputManager inputManager,int index)
        {
            Index = index;
            Click.Init(inputManager, Index);
        }
        public void SetInfo(SavedInfo info)
        {
            Info = info;
            if (IsEmpty == false)
            {

            }
            else
            {

            }
        }
    }
}