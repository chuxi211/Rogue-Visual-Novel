using Input;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu.Input
{
    public class SettingToggle : SettingInput, IToggle
    {
        private Func<bool, bool> ApplyValue;
        private bool PendingValue;

        [SerializeField]
        private Toggle ToggleControl;

        public bool BindFunc(Func<bool, bool> applyValue)
        {
            if (applyValue == null)
            {
                Debug.LogError(
                    "SettingToggle failed to bind function: " +
                    "applyValue is null.");
                return false;
            }

            ApplyValue = applyValue;
            return true;
        }

        public override void Execute()
        {
            Toggle();
        }

        public void Toggle()
        {
            if (ApplyValue == null)
            {
                Debug.LogError(
                    "SettingToggle failed: ApplyValue is null.");
                return;
            }

            if (!ApplyValue(PendingValue))
            {
                Debug.LogError(
                    "SettingToggle failed to apply value.");
            }
        }

        public void OnToggled(bool value)
        {
            Debug.Log(
                $"SettingToggle received input: " +
                $"object={name}, value={value}.",
                this);

            PendingValue = value;
            TryPush();
        }

        public bool SetValue(bool value)
        {
            if (ToggleControl == null)
            {
                Debug.LogError(
                    "SettingToggle failed to set value: " +
                    "Toggle is null.");
                return false;
            }

            ToggleControl.SetIsOnWithoutNotify(value);
            PendingValue = value;
            return true;
        }
    }
}
