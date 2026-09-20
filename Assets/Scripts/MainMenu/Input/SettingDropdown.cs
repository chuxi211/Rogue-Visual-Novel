using Input;
using System;
using TMPro;
using UnityEngine;

namespace MainMenu.Input
{
    public class SettingDropdown : SettingInput, IDropdown
    {
        private Func<int, bool> ApplyValue;
        private int PendingValue;

        [SerializeField]
        private TMP_Dropdown Dropdown;

        public bool BindFunc(Func<int, bool> applyValue)
        {
            if (applyValue == null)
            {
                Debug.LogError(
                    "SettingDropdown failed to bind function: " +
                    "applyValue is null.");
                return false;
            }

            ApplyValue = applyValue;
            return true;
        }

        public override void Execute()
        {
            Select();
        }

        public void Select()
        {
            if (ApplyValue == null)
            {
                Debug.LogError(
                    "SettingDropdown failed: ApplyValue is null.");
                return;
            }

            if (!ApplyValue(PendingValue))
            {
                Debug.LogError(
                    "SettingDropdown failed to apply value.");
            }
        }

        public void OnSelected(int value)
        {
            Debug.Log(
                $"SettingDropdown received input: " +
                $"object={name}, value={value}.",
                this);

            PendingValue = value;
            TryPush();
        }

        public bool SetValue(int value)
        {
            if (Dropdown == null)
            {
                Debug.LogError(
                    "SettingDropdown failed to set value: " +
                    "Dropdown is null.");
                return false;
            }

            if (Dropdown.options == null ||
                value < 0 || value >= Dropdown.options.Count)
            {
                Debug.LogError(
                    "SettingDropdown failed to set value: " +
                    $"value {value} is outside the option range.");
                return false;
            }

            Dropdown.SetValueWithoutNotify(value);
            PendingValue = value;
            return true;
        }
    }
}
