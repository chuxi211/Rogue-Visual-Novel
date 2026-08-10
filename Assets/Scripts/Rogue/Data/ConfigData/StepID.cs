using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Config
{
    [Serializable]
    public struct StepID : IEquatable<StepID>
    {
        [SerializeField]
        private string Value;
        public StepID(string Value)
        {
            this.Value = Value;
        }

        public bool IsValid =>
            Guid.TryParseExact(Value, "N", out _);

        public static StepID Create()
        {
            return new StepID(Guid.NewGuid().ToString("N"));
        }

        public bool Equals(StepID Other)
        {
            return Value == Other.Value;
        }

        public override bool Equals(object Object)
        {
            return Object is StepID Other && Equals(Other);
        }

        public override int GetHashCode()
        {
            return Value == null ? 0 : Value.GetHashCode();
        }

        public static bool operator ==(StepID Left, StepID Right)
        {
            return Left.Equals(Right);
        }

        public static bool operator !=(StepID Left, StepID Right)
        {
            return !Left.Equals(Right);
        }

        public override string ToString()
        {
            return Value ?? string.Empty;
        }
    }
}
