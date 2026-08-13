using System;

namespace Running
{
    public readonly struct CharacterPlaybackHandle
    {
        public int Value { get; }
        public bool IsValid => Value > 0;
        public CharacterPlaybackHandle(int value) { Value = value; }
        public bool Equals(CharacterPlaybackHandle other) => Value == other.Value;
        public override bool Equals(object obj) => obj is CharacterPlaybackHandle other && Equals(other);
        public override int GetHashCode() => Value;
    }
}