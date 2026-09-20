using Input;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu.Input
{
    public class SettingSlider : SettingInput, ISlider
    {
        private Func<float, bool> ApplyValue;
        private float PendingValue;

        [SerializeField]
        private Slider Slider;

        public bool BindFunc(Func<float, bool> applyValue)
        {
            if (applyValue == null)
            {
                Debug.LogError(
                    "SettingSlider failed to bind function: " +
                    "applyValue is null.");
                return false;
            }

            ApplyValue = applyValue;
            return true;
        }

        public override void Execute()
        {
            Slid();
        }

        public void Slid()
        {
            if (ApplyValue == null)
            {
                Debug.LogError(
                    "SettingSlider failed: ApplyValue is null.");
                return;
            }
            if (!ApplyValue(PendingValue))
            {
                Debug.LogError(
                    "SettingSlider failed to apply value.");
            }
        }
        public void OnSlid(float value)
        {
            Debug.Log(
                $"SettingSlider received input: " +
                $"object={name}, value={value}.",
                this);

            if (float.IsNaN(value) || float.IsInfinity(value))
            {
                Debug.LogError(
                    "SettingSlider failed to receive value: " +
                    "value is not finite.");
                return;
            }

            PendingValue = value;
            TryPush();
        }

        public bool SetValue(float value)
        {
            if (Slider == null)
            {
                Debug.LogError(
                    "SettingSlider failed to set value: " +
                    "Slider is null.");
                return false;
            }

            if (float.IsNaN(value) || float.IsInfinity(value))
            {
                Debug.LogError(
                    "SettingSlider failed to set value: " +
                    "value is not finite.");
                return false;
            }

            Slider.SetValueWithoutNotify(value);
            PendingValue = Slider.value;
            if (!Mathf.Approximately(PendingValue, value))
            {
                Debug.LogError(
                    "SettingSlider failed to set value: " +
                    $"Slider range changed {value} to {PendingValue}.");
                return false;
            }

            return true;
        }
    }
}
